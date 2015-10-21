function ManageGroupViewModel(app, datamodel) {
    var groupId = app.Views.ManageGroup.id;
    debugger;

    function initialize() {
        datamodel.getGroups(groupId).success(function (data) {
            alert(data.name);
        }).then(function () {
            //datamodel.getTeachers()
        });
    }

    var vm = {
        group: ko.observable(),
        subjects: ko.observableArray([]),
        rules: ko.observableArray([]),
        teachers: ko.observableArray([])
    };

    initialize();
}

function Subject(id, name, teacherId, type) {
    this.id = ko.observable(id);
    this.name = ko.observable(name);
    this.teacherId = ko.observable(teacherId);
    this.type = ko.observable(type);
}

function Teacher(id, name, middlename, surname) {
    this.id = ko.observable(id);
    this.name = ko.observable(name);
    this.middlename = ko.observable(middlename);
    this.surname = ko.observable(surname);
}

function ScheduleRule(id, startDate, endDate, interval, duration, room) {
    this.id = id;
    this.startDate = ko.observable(startDate);
    this.endDate = ko.observable(endDate);
    this.interval = ko.observable(interval);
    this.duration = ko.observable(dureation);
    this.room = ko.observable(room);
}

app.addViewModel({
    name: "ManageGroup",
    bindingMemberName: "manageGroup",
    factory: ManageGroupViewModel,
    navigatorFactory: function (app) {
        return function (id) {
            app.errors.removeAll();
            app.Views.ManageGroup.id = id;
            app.view(app.Views.ManageGroup);
        };
    }
});