;(function(window, Signa, undefined)
{
    'use strict';

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
            this._state.recognize(frame);
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
        }
    };

    Signa.recognizer.SignRecognizer = SignRecognizer;
})(window, window.Signa);
