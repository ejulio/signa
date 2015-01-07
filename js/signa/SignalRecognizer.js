;(function(window, Signa, undefined)
{
    var RECOGNIZE_EVENT_ID = 'recognize';

    var connection = $.connection;
    connection.hub.url = 'http://localhost:9000/signalr';

    var eventEmitter = new EventEmitter();

    var OfflineSignalRecognizer = {
        _eventEmitter: eventEmitter,

        recognize: function(){},

        addRecognizeEventListener: function(listener)
        {
            this._eventEmitter.addListener(RECOGNIZE_EVENT_ID, listener);
        }
    };

    var OnlineSignalRecognizer = {
        _eventEmitter: eventEmitter,

        recognize: function(signalParameters, signalId)
        {
            var me = this;
            connection
                .recognizer
                .server
                .recognize(signalParameters, signalId)
                .then(function(signalRecognized)
                {
                    if (signalRecognized)
                    {
                        me._eventEmitter.trigger(RECOGNIZE_EVENT_ID);
                    }
                });;
        },

        addRecognizeEventListener: function(listener)
        {
            this._eventEmitter.addListener(RECOGNIZE_EVENT_ID, listener);
        }
    };

    var recognizerHub = OfflineSignalRecognizer;

    connection.hub.start().done(function()
    {
        recognizerHub = OnlineSignalRecognizer;
    });

    function SignalRecognizer(leapController)
    {
        leapController.on('frame', this._onLeapFrame.bind(this));
        this._eventEmitter = new EventEmitter();
    }

    SignalRecognizer.prototype = {
        _eventEmitter: undefined,
        _signalToRecognizeId: 0,

        setSignalToRecognizeId: function(signalToRecognizeId)
        {
            this._signalToRecognizeId = signalToRecognizeId;
        },

        _recognize: function(frame)
        {
            var signalParameters = { name: "Hand", id: 1 };
            recognizerHub.recognize(signalParameters, this._signalToRecognizeId);
            //debugger;
        },

        _onLeapFrame: function(frame)
        {
            // pegar os dados necessários do frame
            this._recognize(frame);
        },

        addRecognizeEventListener: function(listener)
        {
            recognizerHub.addRecognizeEventListener(listener);
        }
    };

    Signa.SignalRecognizer = SignalRecognizer;
})(window, window.Signa);
