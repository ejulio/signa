;(function(window, Signa, undefined)
{
    function OfflineSignalRecognizer(eventEmitter)
    {
        this._eventEmitter = eventEmitter;
    }

    OfflineSignalRecognizer.prototype = {
        _eventEmitter: undefined,

        addRecognizeEventListener: function(listener)
        {
            this._eventEmitter.addListener(Signa.SignalRecognizer.RECOGNIZE_EVENT_ID, listener);
        },

        recognize: function(){},

        setSignalToRecognizeId: function(){},

        save: function(){},

        train: function(){}
    };

    Signa.OfflineSignalRecognizer = OfflineSignalRecognizer;
})(window, window.Signa);
