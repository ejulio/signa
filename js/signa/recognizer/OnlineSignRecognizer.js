;(function(window, Signa, undefined)
{
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

        setSignToRecognizeId: function(){},

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

    Signa.recognizer.OnlineSignRecognizer = OnlineSignRecognizer;
})(window, window.Signa);
