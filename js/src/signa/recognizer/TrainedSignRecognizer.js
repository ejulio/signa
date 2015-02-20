;(function(window, Signa, undefined)
{
    'use strict';

    function TrainedSignRecognizer(eventEmitter)
    {
        this._eventEmitter = eventEmitter;
    }

    TrainedSignRecognizer.prototype = {
        _eventEmitter: undefined,
        _signToReconizeId: -1,

        addRecognizeEventListener: function(listener)
        {
            this._eventEmitter.addListener(Signa.recognizer.SignRecognizer.RECOGNIZE_EVENT_ID, listener);
        },

        recognize: function(frame)
        {
            if (!frame.hands.length)
                return;
                
            var data = new Signa.recognizer.FrameSignDataProcessor().process(frame);

            Signa.signalrHub()
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
        }
    };

    Signa.recognizer.TrainedSignRecognizer = TrainedSignRecognizer;
})(window, window.Signa);
