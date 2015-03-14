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

        me._estado = me.OFFLINE;

        Signa.Hubs.iniciar().done(function()
        {
            me._estado = me.TRAINED;
        });
    }

    SignRecognizer.prototype = {
        RECOGNIZE_EVENT_ID: 'recognize',
        OFFLINE: undefined,
        ONLINE: undefined,
        TRAINED: undefined,

        _eventEmitter: undefined,
        _estado: undefined,
        _frame: undefined,
        _idDoSinalParaReconhecer: -1,

        setState: function(state)
        {
            this._estado = state;
            state.setSignalToRecognizeId(this._idDoSinalParaReconhecer);
        },

        setSignToRecognizeId: function(signalToRecognizeId)
        {
            this._estado.setSignToRecognizeId(signalToRecognizeId);
            this._idDoSinalParaReconhecer = signalToRecognizeId;
        },

        _reconhecer: function(frame)
        {
            this._estado.recognize(frame);
        },

        _onLeapFrame: function(frame)
        {
            // pegar os dados necessários do frame
            this._frame = frame;
            this._reconhecer(frame);
        },

        addRecognizeEventListener: function(listener)
        {
            this._estado.addRecognizeEventListener(listener);
        }
    };

    Signa.recognizer.SignRecognizer = SignRecognizer;
})(window, window.Signa);
