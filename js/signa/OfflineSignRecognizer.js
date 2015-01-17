;(function(window, Signa, undefined)
{
    function OfflineSignRecognizer(eventEmitter)
    {
        this._eventEmitter = eventEmitter;
    }

    OfflineSignRecognizer.prototype = {
        _eventEmitter: undefined,

        addRecognizeEventListener: function(listener)
        {
            this._eventEmitter.addListener(Signa.SignRecognizer.RECOGNIZE_EVENT_ID, listener);
        },

        recognize: function(){},

        setSignalToRecognizeId: function(){},

        save: function(){},

        train: function(){}
    };

    Signa.OfflineSignRecognizer = OfflineSignRecognizer;
})(window, window.Signa);
