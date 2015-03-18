;(function(window, Signa, undefined)
{
    'use strict';

    function ReconhecedorDeSinais(leapController)
    {
        var me = this;
        leapController.on('frame', this._onLeapFrame.bind(this));
        me._eventEmitter = new EventEmitter();
        me.OFFLINE = new Signa.reconhecimento.ReconhecedorDeSinaisOffline(this._eventEmitter);
        me.ONLINE = new Signa.reconhecimento.ReconhecedorDeSinaisOnline(this._eventEmitter);

        me._estado = me.OFFLINE;

        Signa.Hubs.iniciar()
            .done(function()
            {
                me._estado = me.ONLINE;
            });
    }

    ReconhecedorDeSinais.prototype = {
        RECOGNIZE_EVENT_ID: 'recognize',
        OFFLINE: undefined,
        ONLINE: undefined,

        _eventEmitter: undefined,
        _estado: undefined,
        _frame: undefined,
        _idDoSinalParaReconhecer: -1,

        setIdDoSinalParaReconhecer: function(signalToRecognizeId)
        {
            this._estado.setIdDoSinalParaReconhecer(signalToRecognizeId);
            this._idDoSinalParaReconhecer = signalToRecognizeId;
        },

        setTipoDoSinal: function(tipoDoSinal) {
            this._estado.setTipoDoSinal(tipoDoSinal);
        },

        _reconhecer: function()
        {
            this._estado.reconhecer(this._frame);
        },

        _onLeapFrame: function(frame)
        {
            this._frame = frame;
            this._reconhecer();
        },

        adicionarListenerDeReconhecimento: function(listener)
        {
            this._estado.adicionarListenerDeReconhecimento(listener);
        }
    };

    Signa.reconhecimento.ReconhecedorDeSinais = ReconhecedorDeSinais;
})(window, window.Signa);
