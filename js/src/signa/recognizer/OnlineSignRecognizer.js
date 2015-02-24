;(function(window, Signa, undefined)
{
    'use strict';

    function OnlineSignRecognizer(signalRecognizer, eventEmitter)
    {
        this._eventEmitter = eventEmitter;
        this._signalRecognizer = signalRecognizer;
    }

    OnlineSignRecognizer.prototype = {
        _eventEmitter: undefined,
        _signalRecognizer: undefined,

        addRecognizeEventListener: function(listener)
        {
            this._eventEmitter.addListener(Signa.recognizer.SignRecognizer.RECOGNIZE_EVENT_ID, listener);
        },

        recognize: function(){},

        setSignToRecognizeId: function(){}
    };

    Signa.recognizer.OnlineSignRecognizer = OnlineSignRecognizer;
})(window, window.Signa);
