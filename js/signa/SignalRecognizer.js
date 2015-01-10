;(function(window, Signa, undefined)
{
    var RECOGNIZE_EVENT_ID = 'recognize';

    var connection = $.connection;
    connection.hub.url = 'http://localhost:9000/signalr';

    var eventEmitter = new EventEmitter();

    var OfflineSignalRecognizer = {
        _eventEmitter: eventEmitter,

        recognize: function(){},
        save: function(){},
        train: function(){},

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

        save: function(signalParameters, signalId)
        {
            connection
                .recognizer
                .server
                .saveSignalParameters(signalParameters, signalId)
        },

        train: function()
        {
            connection
                .recognizer
                .server
                .trainRecognizer();
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
        _save: false,

        setSignalToRecognizeId: function(signalToRecognizeId)
        {
            this._signalToRecognizeId = signalToRecognizeId;
        },

        _recognize: function(frame)
        {
            var hand = frame.hands[0];
            if (hand)
            {
                var anglesBetweenFingers = [];
                var length = hand.fingers.length - 1;
                for (var i = 0; i < length; i++)
                {
                    var origin = new THREE.Vector3();
                    var destiny = new THREE.Vector3();

                    origin.fromArray(hand.fingers[i].tipPosition);
                    destiny.fromArray(hand.fingers[i + 1].tipPosition);

                    anglesBetweenFingers.push(origin.angleTo(destiny));
                }

                var signalParameters = {
                    palmNormal: hand.palmNormal,
                    handDirection: hand.direction,
                    anglesBetweenFingers: anglesBetweenFingers
                };

                if (this._save)
                {
                    recognizerHub.save(signalParameters, this._signalToRecognizeId);
                    this._save = false;
                }
                else
                {
                    recognizerHub.recognize(signalParameters, this._signalToRecognizeId);
                }
                //debugger;
            }
        },

        _onLeapFrame: function(frame)
        {
            // pegar os dados necessários do frame
            this._recognize(frame);
        },

        addRecognizeEventListener: function(listener)
        {
            recognizerHub.addRecognizeEventListener(listener);
        },

        save: function(id)
        {
            this._signalToRecognizeId = id;
            this._save = true;
        },

        train: function()
        {
            recognizerHub.train();
        }
    };

    Signa.SignalRecognizer = SignalRecognizer;
})(window, window.Signa);
