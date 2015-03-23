;(function(window, Signa, undefined) {
    'use strict';

    function ReconhecedorDeSinaisOffline(eventEmitter) {
        this._eventEmitter = eventEmitter;
    }

    ReconhecedorDeSinaisOffline.prototype = {
        _eventEmitter: undefined,

        adicionarListenerDeReconhecimento: function(listener) {
            this._eventEmitter.addListener(Signa.reconhecimento.ReconhecedorDeSinais.RECOGNIZE_EVENT_ID, listener);
        },

        reconhecer: function(){},

        setIdDoSinalParaReconhecer: function(){},

        setTipoDoSinal: function(){}
    };

    Signa.reconhecimento.ReconhecedorDeSinaisOffline = ReconhecedorDeSinaisOffline;
})(window, window.Signa);
