$(function () {
    var nua = navigator.userAgent;
    var isAndroid = (nua.indexOf('Mozilla/5.0') > -1 && nua.indexOf('Android ') > -1 && nua.indexOf('AppleWebKit') > -1 && nua.indexOf('Chrome') === -1);
    if (isAndroid) {
        $('select.form-control').removeClass('form-control').css('width', '100%');
    }

    ko.applyBindings(StandardEncounterDeploymentViewModel);

    StandardEncounterDeploymentViewModel.FlipAgain();
});

function StandardEncounterDeploymentViewModel() {
    var self = this;
    self.Deployment = {
        Name: ko.observable(''),
        Description: ko.observable(''),
    };
    self.Strategy = {
        Name: ko.observable(''),
        Setup: ko.observable(''),
        VictoryPoints: ko.observable(''),
    };
    self.Scheme1 = {
        Name: ko.observable(''),
        Description: ko.observable(''),
    };
    self.Scheme2 = {
        Name: ko.observable(''),
        Description: ko.observable(''),
    };
    self.Scheme3 = {
        Name: ko.observable(''),
        Description: ko.observable(''),
    };
    self.Scheme4 = {
        Name: ko.observable(''),
        Description: ko.observable(''),
    };
    self.Scheme5 = {
        Name: ko.observable(''),
        Description: ko.observable(''),
    };
    self.FlipAgain = function () {

    }
};

if (navigator.userAgent.match(/IEMobile\/10\.0/)) {
    var msViewportStyle = document.createElement('style');
    msViewportStyle.appendChild(
        document.createTextNode(
            '@-ms-viewport{width:auto!important}'
        )
    );
    document.querySelector('head').appendChild(msViewportStyle);
}