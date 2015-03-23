;(function(window, Signa, undefined) {
    'use strict';

    function ReconhecedorDeSinais(frameBuffer) {
        var me = this,
            eventEmitter = new EventEmitter();
        
        me.OFFLINE = new Signa.reconhecimento.ReconhecedorDeSinaisOffline(this._eventEmitter);
        me.ONLINE = new Signa.reconhecimento.ReconhecedorDeSinaisOnline(this._eventEmitter);
        me._estado = me.OFFLINE;

        frameBuffer.adicionarListenerDeFrame(this._onFrame.bind(this));

        Signa.Hubs.iniciar()
            .done(function() {
                me._estado = me.ONLINE;
            });
    }

    ReconhecedorDeSinais.prototype = {
        RECOGNIZE_EVENT_ID: 'reconheceu',
        OFFLINE: undefined,
        ONLINE: undefined,

        _estado: undefined,

        setIdDoSinalParaReconhecer: function(idDoSinalParaReconhecer) {
            this._estado.setIdDoSinalParaReconhecer(idDoSinalParaReconhecer);
        },

        setTipoDoSinal: function(tipoDoSinal) {
            this._estado.setTipoDoSinal(tipoDoSinal);
        },
        
        _onFrame: function(frame) {
            this._estado.reconhecer(frame);
        },

        adicionarListenerDeReconhecimento: function(listener) {
            this._estado.adicionarListenerDeReconhecimento(listener);
        }
    };

    Signa.reconhecimento.ReconhecedorDeSinais = ReconhecedorDeSinais;
})(window, window.Signa);
