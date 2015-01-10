;(function(window, Signa, undefined)
{
    var recognizerHub = $.connection.recognizer.server;

    function TrainedSignalRecognizer(eventEmitter)
    {
        this._eventEmitter = eventEmitter;
    }

    TrainedSignalRecognizer.prototype = {
        _eventEmitter: undefined,
        _signalToReconizeId: -1,

        addRecognizeEventListener: function(listener)
        {
            this._eventEmitter.addListener(Signa.SignalRecognizer.RECOGNIZE_EVENT_ID, listener);
        },

        recognize: function(signalData)
        {
            recognizerHub
                .recognize(signalData)
                .then(function(signalRecognizedId)
                {
                    if (signalRecognizedId === this._signalToReconizeId)
                    {
                        this._eventEmitter.trigger(Signa.SignalRecognizer.RECOGNIZE_EVENT_ID);
                    }
                }.bind(this));
        },

        setSignalToRecognizeId: function(id)
        {
            this._signalToReconizeId = id;
        },

        save: function(signalData)
        {
            recognizerHub.save(signalData);
        },

        train: function()
        {
            recognizerHub.train().done();
        }
    };

    Signa.TrainedSignalRecognizer = TrainedSignalRecognizer;
})(window, window.Signa);
