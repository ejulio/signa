;(function(window, Signa, undefined)
{
    'use strict';

    function TrainedSignRecognizer(eventEmitter)
    {
        this._eventEmitter = eventEmitter;
        this._frameSignDataProcessor = new Signa.recognizer.FrameSignDataProcessor();
    }

    TrainedSignRecognizer.prototype = {
        _eventEmitter: undefined,
        _frameSignDataProcessor: undefined,
        _signToReconizeId: -1,

        addRecognizeEventListener: function(listener)
        {
            this._eventEmitter.addListener(Signa.recognizer.SignRecognizer.RECOGNIZE_EVENT_ID, listener);
        },

        recognize: function(frame)
        {
            if (!frame.hands.length)
                return;
                
            var data = this._frameSignDataProcessor.extrairFrameDaAmostra(frame);

            Signa.Hubs
                .staticSignRecognizer()
                .recognize(data)
                .then(function(signalRecognizedId)
                {
                    if (signalRecognizedId === this._signToReconizeId)
                    {
                        this._eventEmitter.trigger(Signa.recognizer.SignRecognizer.RECOGNIZE_EVENT_ID);
                    }
                }.bind(this));
        },

        setSignToRecognizeId: function(id)
        {
            this._signToReconizeId = id;
        },

        setTipoDoSinal: function(tipoDoSinal) {

        }
    };

    Signa.recognizer.TrainedSignRecognizer = TrainedSignRecognizer;
})(window, window.Signa);
