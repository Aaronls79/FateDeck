$(function () {
    var nua = navigator.userAgent;
    var isAndroid = (nua.indexOf('Mozilla/5.0') > -1 && nua.indexOf('Android ') > -1 && nua.indexOf('AppleWebKit') > -1 && nua.indexOf('Chrome') === -1);
    if (isAndroid) {
        $('select.form-control').removeClass('form-control').css('width', '100%');
    }

    standardEncounterDeploymentViewModel = new StandardEncounterDeploymentViewModel();
    ko.applyBindings(standardEncounterDeploymentViewModel);

    standardEncounterDeploymentViewModel.Flip();
});

function StandardEncounterDeploymentViewModel() {
    var self = this;
    self.Loading = ko.observable(true);
    self.ShowEncouter = ko.observable(false);
    self.Deployment = {
        Name: ko.observable(''),
        Description: ko.observable('')
    };
    self.Strategy = {
        Name: ko.observable(''),
        Setup: ko.observable(''),
        VictoryPoints: ko.observable('')
    };
    self.Scheme1 = {
        Name: ko.observable(''),
        Description: ko.observable('')
    };
    self.Scheme2 = {
        Name: ko.observable(''),
        Description: ko.observable('')
    };
    self.Scheme3 = {
        Name: ko.observable(''),
        Description: ko.observable('')
    };
    self.Scheme4 = {
        Name: ko.observable(''),
        Description: ko.observable('')
    };
    self.Scheme5 = {
        Name: ko.observable(''),
        Description: ko.observable('')
    };
    self.Flip = function () {
        self.Loading(true);
        self.ShowEncouter(false);
        $.getJSON('/api/standardencounter', function (data) {
            self.Deployment.Description(data.Deployment.Description);
            self.Deployment.Name(data.Deployment.Name);
            self.Strategy.Name(data.Strategy.Name);
            self.Strategy.Setup(data.Strategy.Setup);
            self.Strategy.VictoryPoints(data.Strategy.VictoryPoints);
            self.Scheme1.Name(data.Schemes[0].Name);
            self.Scheme1.Description(data.Schemes[0].Description);
            self.Scheme2.Name(data.Schemes[1].Name);
            self.Scheme2.Description(data.Schemes[1].Description);
            self.Scheme3.Name(data.Schemes[2].Name);
            self.Scheme3.Description(data.Schemes[2].Description);
            self.Scheme4.Name(data.Schemes[3].Name);
            self.Scheme4.Description(data.Schemes[3].Description);
            self.Scheme5.Name(data.Schemes[4].Name);
            self.Scheme5.Description(data.Schemes[4].Description);
            self.Loading(false);
            self.ShowEncouter(true);
        });
    }
};
var standardEncounterDeploymentViewModel;

if (navigator.userAgent.match(/IEMobile\/10\.0/)) {
    var msViewportStyle = document.createElement('style');
    msViewportStyle.appendChild(
        document.createTextNode(
            '@-ms-viewport{width:auto!important}'
        )
    );
    document.querySelector('head').appendChild(msViewportStyle);
}