;(function(window, Signa, undefined)
{
    function TrainedSignRecognizer(eventEmitter)
    {
        this._eventEmitter = eventEmitter;
    }

    TrainedSignRecognizer.prototype = {
        _eventEmitter: undefined,
        _signalToReconizeId: -1,

        addRecognizeEventListener: function(listener)
        {
            this._eventEmitter.addListener(Signa.recognizer.SignRecognizer.RECOGNIZE_EVENT_ID, listener);
        },

        recognize: function(signalData)
        {
            Signa.HUB
                .recognize(signalData)
                .then(function(signalRecognizedId)
                {
                    if (signalRecognizedId === this._signalToReconizeId)
                    {
                        this._eventEmitter.trigger(Signa.recognizer.SignRecognizer.RECOGNIZE_EVENT_ID);
                    }
                }.bind(this));
        },

        setSignalToRecognizeId: function(id)
        {
            this._signalToReconizeId = id;
        },

        save: function(signalData)
        {
            Signa.HUB.save(signalData);
        },

        train: function()
        {
            Signa.HUB.train().done();
        }
    };

    Signa.recognizer.TrainedSignRecognizer = TrainedSignRecognizer;
})(window, window.Signa);
