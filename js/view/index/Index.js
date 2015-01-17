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

        init: function()
        {
            var width = $("#handmodel-user").width(),
                height = $("#handmodel-user").height(),
                container = $("#handmodel-example"),
                defaultCameraFactory = new Signa.camera.DefaultCameraFactory(width / height),
                leapController = new Leap.Controller(),
                leapController2 = new Leap.Controller();

            this._signDescription = new View.index.SignDescription($('#sign-description'));
            this._signExample = new View.index.SignExample(defaultCameraFactory, container, leapController, width, height);
            this._userHands = new View.index.UserHands(defaultCameraFactory, $("#handmodel-user"), leapController2, width, height);

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
        },

        _onRecognize: function()
        {
            this._signDescription.onRecognize();
            this._signExample.onRecognize();

            window.setTimeout(this._loadNextSign.bind(this), 500);
        },

        _loadNextSign: function()
        {
            console.log('carregando próximo sinal');
            Signa.initHubs().done(function()
            {
                Signa.HUB
                    .getNextSign(this._signInfo ? this._signInfo.Id : -1)
                    .done(this._onNewSign.bind(this));
            }.bind(this));
        }
    };


    View.index.Index = Index;
})(window, window.View, window.Signa);
