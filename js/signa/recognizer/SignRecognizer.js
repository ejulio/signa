;(function(window, Signa, undefined)
{
    function SignRecognizer(leapController)
    {
        var me = this;
        leapController.on('frame', this._onLeapFrame.bind(this));
        me._eventEmitter = new EventEmitter();
        me.OFFLINE = new Signa.recognizer.OfflineSignRecognizer(this._eventEmitter);
        me.ONLINE = new Signa.recognizer.OnlineSignRecognizer(this, this._eventEmitter);
        me.TRAINED = new Signa.recognizer.TrainedSignRecognizer(this._eventEmitter);

        me._state = me.OFFLINE;

        Signa.initHubs().done(function()
        {
            me._state = me.TRAINED;
        });
    }

    SignRecognizer.prototype = {
        RECOGNIZE_EVENT_ID: 'recognize',
        OFFLINE: undefined,
        ONLINE: undefined,
        TRAINED: undefined,

        _eventEmitter: undefined,
        _state: undefined,
        _frame: undefined,
        _signToRecognizeId: -1,

        setState: function(state)
        {
            this._state = state;
            state.setSignalToRecognizeId(this._signToRecognizeId);
        },

        setSignToRecognizeId: function(signalToRecognizeId)
        {
            this._state.setSignToRecognizeId(signalToRecognizeId);
            this._signToRecognizeId = signalToRecognizeId;
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

                this._state.recognize(signalParameters);
                //debugger;
            }
        },

        _onLeapFrame: function(frame)
        {
            // pegar os dados necessários do frame
            this._frame = frame;
            this._recognize(frame);
        },

        addRecognizeEventListener: function(listener)
        {
            this._state.addRecognizeEventListener(listener);
        },

        save: function(id)
        {
            var hand = this._frame.hands[0];
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
                    id: id,
                    palmNormal: hand.palmNormal,
                    handDirection: hand.direction,
                    anglesBetweenFingers: anglesBetweenFingers
                };

                this._state.save(signalParameters);
            }
        },

        train: function()
        {
            this._state.train();
        }
    };

    Signa.recognizer.SignRecognizer = SignRecognizer;
})(window, window.Signa);
