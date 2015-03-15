;(function(window, Signa, undefined)
{
    'use strict';

    function OfflineSignRecognizer(eventEmitter)
    {
        this._eventEmitter = eventEmitter;
    }

    OfflineSignRecognizer.prototype = {
        _eventEmitter: undefined,

        addRecognizeEventListener: function(listener)
        {
            this._eventEmitter.addListener(Signa.recognizer.SignRecognizer.RECOGNIZE_EVENT_ID, listener);
        },

        recognize: function(){},

        setSignToRecognizeId: function(){},

        setTipoDoSinal: function(){}
    };

    Signa.recognizer.OfflineSignRecognizer = OfflineSignRecognizer;
})(window, window.Signa);
