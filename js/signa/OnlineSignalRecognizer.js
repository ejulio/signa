;(function(window, Signa, undefined)
{
    function OnlineSignalRecognizer(signalRecognizer, eventEmitter)
    {
        this._eventEmitter = eventEmitter;
        this._signalRecognizer = signalRecognizer;
    }

    OnlineSignalRecognizer.prototype = {
        _eventEmitter: undefined,
        _signalRecognizer: undefined,

        addRecognizeEventListener: function(listener)
        {
            this._eventEmitter.addListener(Signa.SignalRecognizer.RECOGNIZE_EVENT_ID, listener);
        },

        recognize: function(){},

        setSignalToRecognizeId: function(){},

        save: function(signalData)
        {
            Signa.HUB.save(signalData);
        },

        train: function()
        {
            console.log('Mudando para o estado treinado');
            this._signalRecognizer.setState(this._signalRecognizer.TRAINED);
        }
    };

    Signa.OnlineSignalRecognizer = OnlineSignalRecognizer;
})(window, window.Signa);
