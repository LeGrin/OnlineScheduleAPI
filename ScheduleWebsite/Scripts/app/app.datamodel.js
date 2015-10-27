function AppDataModel() {
    var self = this,
        // Routes
        addExternalLoginUrl = "/api/Account/AddExternalLogin",
        changePasswordUrl = "/api/Account/changePassword",
        loginUrl = "/Token",
        logoutUrl = "/api/Account/Logout",
        registerUrl = "/api/Account/Register",
        registerExternalUrl = "/api/Account/RegisterExternal",
        removeLoginUrl = "/api/Account/RemoveLogin",
        setPasswordUrl = "/api/Account/setPassword",
        siteUrl = "/",
        userInfoUrl = "/api/Account/UserInfo",
        groupsUrl = "/api/Groups",
        facultyUrl = "api/Faculty",
        teacherUrl = "api/Teacher",
        subjectUrl = "api/Subject",
        scheduleUrl = "api/Schedule";

    // Route operations
    function externalLoginsUrl(returnUrl, generateState) {
        return "/api/Account/ExternalLogins?returnUrl=" + (encodeURIComponent(returnUrl)) +
            "&generateState=" + (generateState ? "true" : "false");
    }

    function manageInfoUrl(returnUrl, generateState) {
        return "/api/Account/ManageInfo?returnUrl=" + (encodeURIComponent(returnUrl)) +
            "&generateState=" + (generateState ? "true" : "false");
    }

    // Other private operations
    function getSecurityHeaders() {
        var accessToken = sessionStorage["accessToken"] || localStorage["accessToken"];

        if (accessToken) {
            return { "Authorization": "Bearer " + accessToken };
        }

        return {};
    }

    // Operations
    self.clearAccessToken = function () {
        localStorage.removeItem("accessToken");
        sessionStorage.removeItem("accessToken");
    };

    self.setAccessToken = function (accessToken, persistent) {
        if (persistent) {
            localStorage["accessToken"] = accessToken;
        } else {
            sessionStorage["accessToken"] = accessToken;
        }
    };

    self.toErrorsArray = function (data) {
        var errors = new Array(),
            items;

        if (!data || !data.message) {
            return null;
        }

        if (data.modelState) {
            for (var key in data.modelState) {
                items = data.modelState[key];

                if (items.length) {
                    for (var i = 0; i < items.length; i++) {
                        errors.push(items[i]);
                    }
                }
            }
        }

        if (errors.length === 0) {
            errors.push(data.message);
        }

        return errors;
    };

    // Data
    self.returnUrl = siteUrl;

    // Data access operations
    self.addExternalLogin = function (data) {
        return $.ajax(addExternalLoginUrl, {
            type: "POST",
            data: data,
            headers: getSecurityHeaders()
        });
    };

    self.changePassword = function (data) {
        return $.ajax(changePasswordUrl, {
            type: "POST",
            data: data,
            headers: getSecurityHeaders()
        });
    };

    self.getExternalLogins = function (returnUrl, generateState) {
        return $.ajax(externalLoginsUrl(returnUrl, generateState), {
            cache: false,
            headers: getSecurityHeaders()
        });
    };

    self.getManageInfo = function (returnUrl, generateState) {
        return $.ajax(manageInfoUrl(returnUrl, generateState), {
            cache: false,
            headers: getSecurityHeaders()
        });
    };

    self.getUserInfo = function (accessToken) {
        var headers;

        if (typeof (accessToken) !== "undefined") {
            headers = {
                "Authorization": "Bearer " + accessToken
            };
        } else {
            headers = getSecurityHeaders();
        }

        return $.ajax(userInfoUrl, {
            cache: false,
            headers: headers
        });
    };

    self.login = function (data) {
        return $.ajax(loginUrl, {
            type: "POST",
            data: data
        });
    };

    self.logout = function () {
        return $.ajax(logoutUrl, {
            type: "POST",
            headers: getSecurityHeaders()
        });
    };

    self.register = function (data) {
        return $.ajax(registerUrl, {
            type: "POST",
            data: data
        });
    };

    self.registerExternal = function (accessToken, data) {
        return $.ajax(registerExternalUrl, {
            type: "POST",
            data: data,
            headers: {
                "Authorization": "Bearer " + accessToken
            }
        });
    };

    self.removeLogin = function (data) {
        return $.ajax(removeLoginUrl, {
            type: "POST",
            data: data,
            headers: getSecurityHeaders()
        });
    };

    self.setPassword = function (data) {
        return $.ajax(setPasswordUrl, {
            type: "POST",
            data: data,
            headers: getSecurityHeaders()
        });
    };


    // Groups and Schedule management
    self.getGroups = function (id) {
        return $.ajax(groupsUrl, {
            type: "GET",
            headers: getSecurityHeaders(),
            data: { id: id }
        });
    };

    self.createGroup = function (data) {
        return $.ajax(groupsUrl, {
            data: { name: data.name, key: data.key, facultyId: data.faculty.id, parentGroupId: data.parentGroup && data.parentGroup.id },
            type: "POST",
            headers: getSecurityHeaders()
        });
    };

    self.getFaculties = function () {
        return $.ajax(facultyUrl, {
            type: "GET",
            headers: getSecurityHeaders()
        });
    };

    self.getTeachers = function (id) {
        return $.ajax({
            url: teacherUrl,
            type: "GET",
            data: id,
            headers: getSecurityHeaders()
        });
    };

    self.createTeacher = function (data) {
        return $.ajax({
            url: teacherUrl,
            type: "POST",
            data: data,
            headers: getSecurityHeaders()
        });
    };

    self.getSubjects = function (id) {
        return $.ajax({
            url: subjectUrl,
            data: { id: id },
            type: "GET"
        });
    };

    self.saveSubject = function (data) {
        data.teacherId = data.teacher.id;
        data.update = null;

        return $.ajax({
            type: data.id ? "PUT" : "POST",
            url: subjectUrl,
            data: data,
            headers: getSecurityHeaders()
        });
    };
}
