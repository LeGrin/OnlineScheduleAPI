function ManageGroupsViewModel(app, dataModel) {
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
            });
        });
        dataModel.getTeachers().success(function (teachers) {
            self.teachers.valueWillMutate();
            _.each(teachers, function (teacher) {
                self.teachers.push(new Teacher(teacher.id, teacher.name, teacher.middlename, teacher.surname));
            });
            self.teachers.valueHasMutated();
        });
    }

    self.groups = ko.observableArray([]);
    self.faculties = ko.observableArray([]);
    self.teachers = ko.observableArray([]);

    self.newGroup = ko.observable();

    self.createNewGroup = function () {
        self.newGroup(new GroupModel(0, "", "", 0));
    }

    self.closeNewGroupDialog = function () {
        self.newGroup({});
    }

    self.saveNewGroup = function () {
        dataModel.createGroup(ko.mapping.toJS(self.newGroup()))
            .success(function (newgroup) {
                var faculty = _.findWhere(self.groups(), { id : newgroup.facultyId });
                self.groups.push(new GroupModel(newgroup.id, newgroup.key, newgroup.name, faculty));
                self.newGroup({});
            })
            .fail(function (error) {
                alert(JSON.stringify(error));
            });
        
    }

    self.openGroup = function (groupClicked) {
        app.navigateToManageGroup(groupClicked.id());
    }
   

    initialize();
}

function GroupModel(id, key, name, faculty) {
    this.id = ko.observable(id);
    this.name = ko.observable(name);
    this.key = ko.observable(key);
    this.faculty = ko.observable(faculty);
}

function FacultyModel(id, name) {
    this.id = id;
    this.name = name;
}

app.addViewModel({
    name: "ManageGroups",
    bindingMemberName: "manageGroups",
    factory: ManageGroupsViewModel,
    navigatorFactory: function (app) {
        return function () {
            app.errors.removeAll();
            app.view(app.Views.ManageGroups);
        }
    }
});