;(function(window, Signa, undefined) {
    'use strict';

    function SinalEstatico() {
        this._hub = Signa.Hubs.sinaisEstaticos();
    }

    SinalEstatico.prototype = {
        _hub: undefined,
        _sinalId: -1,

        setSinalId: function(sinalId) {
            this._sinalId = sinalId;
        },

        reconhecer: function(frame) {
            return this._hub
                .reconhecer([frame])
                .then(function(sinalReconhecidoId) {
                    return sinalReconhecidoId === this._sinalId;
                }.bind(this));
        }
    };

    Signa.reconhecimento.SinalEstatico = SinalEstatico;
})(window, window.Signa);
