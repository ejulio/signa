;(function(window, Signa, undefined) {
    'use strict';

    function AlgoritmoDeSinalEstatico() {
        this._hub = Signa.Hubs.sinaisEstaticos();
    }

    AlgoritmoDeSinalEstatico.prototype = {
        _hub: undefined,
        _sinalId: -1,

        setSinalId: function(sinalId) {
            this._sinalId = sinalId;
            console.log('SINAL ID ' + sinalId);
        },

        reconhecer: function(frame) {
            this._hub
                .reconhecer([frame])
                .then(function(sinalReconhecidoId) {
                    console.log('SINAL RECONHECIDO ' + sinalReconhecidoId);
                    if (sinalReconhecidoId == this._sinalId) {
                        console.log('SUCESSO');
                    }
                }.bind(this));
        }
    };

    Signa.reconhecimento.AlgoritmoDeSinalEstatico = AlgoritmoDeSinalEstatico;
})(window, window.Signa);