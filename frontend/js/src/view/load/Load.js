;(function(window, View, Signa, undefined)
{
    'use strict';

    function Load(){}

    Load.prototype = {
        _leapRecordingPlayer: undefined,
        _loadedFrames: undefined,
        _frameSignDataProcessor: undefined,
        _loadedFramesJson: undefined,

        init: function()
        {
            var leapController = new Leap.Controller();

            Signa.Hubs.init();
            this._initScene(leapController);

            this._leapRecordingPlayer = new Signa.LeapRecordingPlayer(leapController);
            this._frameSignDataProcessor = new Signa.recognizer.FrameSignDataProcessor();

            $('#sign-file').change(this._onSignFileChange.bind(this));
            $('#save').click(this._onSaveClick.bind(this));
        },

        _initScene: function(leapController)
        {
            var width = $("#handmodel-user").width(),
                height = $("#handmodel-user").height(),
                container = $("#handmodel-user"),
                cameraFactory = new Signa.camera.DefaultCameraFactory(width / height),
                userHandmodelScene;

            cameraFactory = new Signa.camera.OrbitControlsCameraFactory(cameraFactory);

            userHandmodelScene = new Signa.scene.Scene(cameraFactory, container, width, height);
            userHandmodelScene = new Signa.scene.RiggedHandScene(leapController, userHandmodelScene);
            
            userHandmodelScene.render();
        },

        _onSignFileChange: function(event)
        {
            var file = event.target.files[0];
            this._readSignFile(file);  
        },

        _readSignFile: function(file)
        {
            var fileReader = new FileReader();

            fileReader.onload = function(event)
            {
                this._loadedFramesJson = event.target.result;
                var loadedFrames = JSON.parse(this._loadedFramesJson);
                
                this._leapRecordingPlayer.loadFrames(loadedFrames, function(frames)
                {
                    this._loadedFrames = frames;
                }.bind(this));
            }.bind(this);

            fileReader.readAsText(file);
        },

        _onSaveClick: function()
        {
            this._saveSignSample();
        },

        _saveSignSample: function()
        {
            var signDescription = $('#description').val(),
                signData = this._getSignDataFromFrame();

            Signa.Hubs
                .staticSignRecognizer()
                .save(signDescription, this._loadedFramesJson, signData);
        },

        _getSignDataFromFrame: function()
        {
            var frame = new Leap.Frame(this._loadedFrames[0]);
            return this._frameSignDataProcessor.process(frame);
        }
    };


    View.load.Load = Load;
})(window, window.View, window.Signa);
