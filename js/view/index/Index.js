;(function(window, View, Signa, undefined)
{
    function Index()
    {

    }

    Index.RECOGNIZE_EVENT_ID = 'recognize';
    Index.NEW_SIGN_EVENT_ID = 'new-sign';

    Index.prototype = {
        _signDescription: undefined,
        _userHands: undefined,
        _signExample: undefined,
        _signRecognizer: undefined,
        _signInfo: undefined,
        _messageBox: undefined,

        init: function()
        {
            var width = $("#handmodel-user").width(),
                height = $("#handmodel-user").height(),
                container = $("#handmodel-example"),
                defaultCameraFactory = new Signa.camera.DefaultCameraFactory(width / height),
                leapController = new Leap.Controller(),
                leapController2 = new Leap.Controller();

            this._messageBox = $('#recognized-sign-message');

            this._signDescription = new View.index.SignDescription($('#sign-description'));
            this._signExample = new View.index.SignExample(defaultCameraFactory, container, leapController, width, height);
            this._userHands = new View.index.UserHands(defaultCameraFactory, $("#handmodel-user"), leapController2, width, height);

            leapController2.connect();

            this._signRecognizer = new Signa.recognizer.SignRecognizer(leapController2)

            this._signRecognizer.addRecognizeEventListener(this._onRecognize.bind(this));

            leapController.on('playback.recordingSet', function()
            {
                this._onNewSignLoad();
            }.bind(this));

            this._loadNextSign();
        },

        _onNewSign: function(signInfo)
        {
            this._signInfo = signInfo;
            this._signExample.onNewSign(signInfo);
        },

        _onNewSignLoad: function()
        {
            this._signDescription.onNewSign(this._signInfo);
            this._signRecognizer.setSignToRecognizeId(this._signInfo.Id);
            this._hideRecognizeMessage();
        },

        _hideRecognizeMessage: function()
        {
            this._messageBox.hide();
        },

        _onRecognize: function()
        {
            this._showRecognizeMessage();
            this._signDescription.onRecognize();
            this._signExample.onRecognize();
            this._signRecognizer.setSignToRecognizeId(-1);
            window.setTimeout(this._loadNextSign.bind(this), 1000);
        },

        _showRecognizeMessage: function()
        {
            this._messageBox.show();
        },

        _loadNextSign: function()
        {
            Signa.initHubs().done(function()
            {
                var signId = this._signInfo ? this._signInfo.Id : -1;
                Signa.HUB
                    .getNextSign(signId)
                    .done(this._onNewSign.bind(this));
            }.bind(this));
        }
    };


    View.index.Index = Index;
})(window, window.View, window.Signa);
