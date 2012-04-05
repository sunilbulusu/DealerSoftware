jQuery.fn.center = function () {
    this.css("position", "fixed");
    this.css("top", (($(window).height() - this.outerHeight()) / 2) + "px");
    this.css("left", (($(window).width() - this.outerWidth()) / 2) + "px");

    return this;
};

jQuery.fn.popup = function (center) {
    if (center || center == undefined || center == null) { this.center(); }
    
    this.slideDown();
};

jQuery.fn.popupWithMarginOffset = function (top, right, bottom, left) {

    if (top != undefined) {
        this.css("margin-top", top);
    }

    if (left != undefined) {
        this.css("margin-left", left);
    }

    this.slideDown();
};

jQuery.fn.closePopup = function (speed) {
    this.slideUp(speed);
    return this;
};

jQuery.fn.alternateBackground = function () {
    var count = 0;
    $("tr", this).each(function () {
        
        if (count % 2 == 0 && count > 0) {
            $(this).toggleClass("alternateRow", true);
        }
        else {
            $(this).toggleClass("alternateRow", false);
        }
        count++;
    });
    //return this;
};

jQuery.fn.validate = function (type) {
    var valid = true;
    var regexS = null;
    var regex = null;
    if (type == 'integer') {
        regexS = "^\\d\\d*\\d$";
    }
    else if (type == 'date') {
        regexS = "^((0?[13578]|10|12)(-|\\/)(([1-9])|(0[1-9])|([12])([0-9]?)|(3[01]?))(-|\\/)((19)([2-9])(\\d{1})|(20)([01])(\\d{1})|([8901])(\\d{1}))|(0?[2469]|11)(-|\\/)(([1-9])|(0[1-9])|([12])([0-9]?)|(3[0]?))(-|\\/)((19)([2-9])(\\d{1})|(20)([01])(\\d{1})|([8901])(\\d{1})))$";
    }
    else if (type == 'email') {
        regexS = "^([\\w\\-\\.]+)@((\\[([0-9]{1,3}\\.){3}[0-9]{1,3}\\])|(([\\w\\-]+\\.)+)([a-zA-Z]{2,4}))$";
    }
    else if (type == 'phone') {
        //regexS = "\\d{3}\\D*\\d{3}\\D*\\d{3}\\d$|\\d{3}\\D*\\d{3}\\D*\\d{4}\\s*(ext|extension)\\s*\\d*\\d$]?";
        regexS = "\\(?\\d{3}\\)?[\\D\\s]*\\d{3}[\\D\\s]*\\d{3}\\d$|\\d{3}[\\D\\s]*\\d{3}[\\D\\s]*\\d{4}\\s*(ext|extension)\\s*\\d*\\d$]?";
    }
    else if (type == 'zipcode') {
        regexS = "^\\d\\d{3}\\d$|\\d{5}\\s*-?\\s*\\d{3}\\d$";
    }
    else {
        regexS = "\\d*";
    }
    regex = new RegExp(regexS);

    var results = regex.exec($(this).val());

    if (results == null || results == "") {
        return false;
    }
    else {
        return true;
    }
};
