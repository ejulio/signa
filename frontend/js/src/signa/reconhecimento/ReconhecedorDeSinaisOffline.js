;(function(window, Signa, undefined) {
    'use strict';

    function ReconhecedorDeSinaisOffline() {}

    ReconhecedorDeSinaisOffline.prototype = {
        _idDoSinalParaReconhecer: -1,
        _tipoDoSinal: -1,

        reconhecer: function() {
            return Promise.resolve(false);
        },

        setIdDoSinalParaReconhecer: function(idDoSinalParaReconhecer) {
            this._idDoSinalParaReconhecer = idDoSinalParaReconhecer;
        },

        setTipoDoSinal: function(tipoDoSinal) {
            this._tipoDoSinal = tipoDoSinal;
        },

        getIdDoSinal: function() {
            return this._idDoSinalParaReconhecer;
        },

        getTipoDoSinal: function() {
            return this._tipoDoSinal;
        }
    };

    Signa.reconhecimento.ReconhecedorDeSinaisOffline = ReconhecedorDeSinaisOffline;
})(window, window.Signa);
