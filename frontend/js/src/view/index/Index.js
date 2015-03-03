;(function(window, View, Signa, undefined)
{
    'use strict';

    function Index(){}

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
                cameraFactory = new Signa.camera.DefaultCameraFactory(width / height);

            this._messageBox = $('#recognized-sign-message');

            this._signDescription = new View.index.SignDescription($('#sign-description'));
            this._initSignExample(cameraFactory, width, height);
            this._initUserHands(cameraFactory, width, height);

            this._loadNextSign();
        },

        _initSignExample: function(cameraFactory, width, height)
        {
            var signExampleLeapController = new Leap.Controller(),
                container = $("#handmodel-example");

            this._signExample = new View.index.SignExample(cameraFactory, container, signExampleLeapController, width, height);

            signExampleLeapController.on('playback.recordingSet', this._onNewSignLoad.bind(this));
        },

        _initUserHands: function(cameraFactory, width, height)
        {
            var userHandsLeapController = new Leap.Controller(),
                container = $("#handmodel-user");

            this._userHands = new View.index.UserHands(cameraFactory, container, userHandsLeapController, width, height);
            
            userHandsLeapController.connect();
            
            this._signRecognizer = new Signa.recognizer.SignRecognizer(userHandsLeapController);
            this._signRecognizer.addRecognizeEventListener(this._onRecognize.bind(this));
        },

        _onNewSign: function(signInfo)
        {
            this._signInfo = signInfo;
            this._signExample.onNewSign(signInfo);
        },

        _onNewSignLoad: function()
        {
            this._signDescription.onNewSign(this._signInfo);
            this._userHands.onNewSign();
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
            this._userHands.onRecognize();
            this._signRecognizer.setSignToRecognizeId(-1);
            window.setTimeout(this._loadNextSign.bind(this), 1000);
        },

        _showRecognizeMessage: function()
        {
            this._messageBox.show();
        },

        _loadNextSign: function()
        {
            Signa.Hubs.init().done(function()
            {
                var signId = this._signInfo ? this._signInfo.Id : -1;
                Signa.Hubs
                    .signSequence()
                    .getNextSign(signId)
                    .done(this._onNewSign.bind(this));
            }.bind(this));
        }
    };


    View.index.Index = Index;
})(window, window.View, window.Signa);
