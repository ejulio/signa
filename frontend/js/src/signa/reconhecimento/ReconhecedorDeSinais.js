;(function(window, Signa, undefined) {
    'use strict';

    var EVENTO_RECONHECEU_SINAL = 'reconheceu';

    function ReconhecedorDeSinais(frameBuffer) {
        var me = this;
        
        me._estado = new Signa.reconhecimento.ReconhecedorDeSinaisOffline();
        me._eventEmitter = new EventEmitter();

        frameBuffer.adicionarListenerDeFrame(this._onFrame.bind(this));

        Signa.Hubs.iniciar()
            .done(function() {
                var tipoDoSinal = me._estado.getTipoDoSinal(),
                    idDoSinal = me._estado.getIdDoSinal();

                me._estado = new Signa.reconhecimento.ReconhecedorDeSinaisOnline();

                me.setTipoDoSinal(tipoDoSinal);
                me.setIdDoSinalParaReconhecer(idDoSinal);
            });
    }

    ReconhecedorDeSinais.prototype = {
        _estado: undefined,
        _eventEmitter: undefined,

        adicionarListenerDeReconhecimento: function(listener) {
            this._eventEmitter.addListener(EVENTO_RECONHECEU_SINAL, listener);
        },

        setIdDoSinalParaReconhecer: function(idDoSinalParaReconhecer) {
            this._estado.setIdDoSinalParaReconhecer(idDoSinalParaReconhecer);
        },

        setTipoDoSinal: function(tipoDoSinal) {
            this._estado.setTipoDoSinal(tipoDoSinal);
        },
        
        _onFrame: function(frame) {
            this._estado
                .reconhecer(frame)
                .then(function(sinalFoiReconhecido) {
                    if (sinalFoiReconhecido) {
                        this._eventEmitter.trigger(EVENTO_RECONHECEU_SINAL);
                    }
                }.bind(this));
        }
    };

    Signa.reconhecimento.ReconhecedorDeSinais = ReconhecedorDeSinais;
})(window, window.Signa);
