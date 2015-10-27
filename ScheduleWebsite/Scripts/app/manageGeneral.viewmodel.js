function ManageGeneralViewModel(app, dataModel) {
    var self = this;

    function initialize() {
        dataModel.getFaculties().done(function (faculties) {

            _.each(faculties, function (faculty) {
                self.faculties.push(new FacultyModel(faculty.id, faculty.name));
            });

            dataModel.getGroups().done(function (data) {
                if (_.isArray(data))
                    self.groups.valueWillMutate();
                _.each(data, function (item) {
                    var faculty = _.findWhere(self.faculties(), { id: item.facultyId });
                    self.groups.push(new GroupModel(item.id, item.key, item.name, faculty));
                });
                self.groups.valueHasMutated();

                // setting parent groups
                _.each(data, function (group) {
                    group.parentGroupId = ~~group.parentGroupId;

                    if (group.parentGroupId) {
                        var existGroup = _.find(self.groups(), function (gr) {
                            return gr.id() == group.id;
                        });

                        var parentGroup = _.find(self.groups(), function (gr) {
                            return gr.id() == group.parentGroupId;
                        });

                        if (existGroup && parentGroup) {
                            existGroup.setParentGroup(parentGroup);
                        }
                    }
                });
            });
        });

        dataModel.getTeachers().success(function (teachers) {
            self.teachers.valueWillMutate();
            _.each(teachers, function (teacher) {
                self.teachers.push(new TeacherModel(teacher.id, teacher.name, teacher.middleName, teacher.surname));
            });
            self.teachers.valueHasMutated();
        })
            .done(function () {
                dataModel.getSubjects().success(function (subjects) {
                    _.each(subjects, function (subject) {
                        var teacher = findById(subject.teacherId, self.teachers(), "id", true);
                        self.subjects.push(new SubjectModel(subject.id, subject.name, subject.type, teacher));
                    });
                })
            });
    }

    self.groups = ko.observableArray([]);
    self.faculties = ko.observableArray([]);
    self.teachers = ko.observableArray([]);
    self.subjects = ko.observableArray([]);

    self.newGroup = ko.observable();
    self.newTeacher = ko.observable();
    self.editSubject = ko.observable();


    self.createNewGroup = function () {
        self.newGroup(new GroupModel(0, "", "", 0));
    }

    self.createNewTeacher = function () {
        self.newTeacher(new TeacherModel(0, '', '', ''));
    }

    self.createNewSubject = function () {
        self.editSubject(new SubjectModel(0, '', 0, null));
    }

    self.closeNewGroupDialog = function () {
        self.newGroup({});
    };

    self.closeNewTeacherDialog = function () {
        self.newTeacher({});
    };

    self.closeSubjectEditDialog = function () {
        self.editSubject({});
    };


    self.saveNewGroup = function () {
        dataModel.createGroup(ko.mapping.toJS(self.newGroup()))
            .success(function (newgroup) {
                var faculty = _.findWhere(self.groups(), { id: newgroup.facultyId });
                self.groups.push(new GroupModel(newgroup.id, newgroup.key, newgroup.name, faculty));
                closeNewGroupDialog();
            })
            .fail(function (error) {
                alert(JSON.stringify(error));
            });

    };

    self.saveNewTeacher = function () {
        dataModel.createTeacher(ko.mapping.toJS(self.newTeacher()))
            .success(function (newTeacher) {
                self.teachers.push(new TeacherModel(newTeacher.id, newTeacher.name, newTeacher.middleName, newTeacher.surname));
                self.closeNewTeacherDialog();
            });
    };

    self.saveSubject = function () {
        var a = dataModel.saveSubject(ko.mapping.toJS(self.editSubject()))
            .success(function (savedSubject) {
                var existSubject = findById(savedSubject.id, self.subjects(), "id", true);
                var teacher = findById(savedSubject.teacherId, self.teachers(), "id", true);
                if (existSubject) {
                    existSubject.update(savedSubject, teacher);
                }
                else {
                    self.subjects.push(new SubjectModel(savedSubject.id, savedSubject.name, savedSubject.type, teacher));
                }
            });
    };

    function findById(id, collection, propertyName, isObservable) {
        return _.find(collection, function (item) {
            return id == (isObservable ? item[propertyName]() : item[propertyName]);
        });
    }

    self.openGroup = function (groupClicked) {
        app.navigateToManageGroup(groupClicked.id());
    };


    initialize();
};

function GroupModel(id, key, name, faculty) {
    this.id = ko.observable(id);
    this.name = ko.observable(name);
    this.key = ko.observable(key);
    this.faculty = ko.observable(faculty);
    this.parentGroup = ko.observable();
};

GroupModel.prototype.setParentGroup = function (parentGroup) {
    this.parentGroup(parentGroup);
};


function FacultyModel(id, name) {
    this.id = id;
    this.name = name;
};


function TeacherModel(id, name, middleName, surname) {
    self = this;

    this.id = ko.observable(id);
    this.name = ko.observable(name);
    this.middlename = ko.observable(middleName);
    this.surname = ko.observable(surname);

    this.presentName = ko.computed(function () {
        return self.surname() + " " + self.name() + " " + self.middlename();
    });
};

function SubjectModel(id, name, type, teacher) {
    if (id && _.isEmpty(teacher))
        throw new Error("Teacher is not defined");

    var self = this;
    this.id = ko.observable(id);
    this.name = ko.observable(name);
    this.type = ko.observable(type);
    this.teacher = ko.observable(teacher);

    this.presentType = ko.computed(function () {
        return self.subjectTypes[~~self.type()];
    });
};

SubjectModel.prototype.subjectTypes = ["Lecture", "Seminar"];

SubjectModel.prototype.update = function (subject, newTeacher) {
    this.name(subject.name);
    this.type(subject.type);
    this.teacher(newTeacher);
};


(function () {

    app.addViewModel({
        name: "ManageGeneral",
        bindingMemberName: "manageGeneral",
        factory: ManageGeneralViewModel,
        navigatorFactory: function (app) {
            return function () {
                app.errors.removeAll();
                app.view(app.Views.ManageGeneral);
            }
        }
    });
})();