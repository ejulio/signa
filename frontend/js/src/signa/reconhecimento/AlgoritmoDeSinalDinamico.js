;(function(window, Signa, undefined) {
    'use strict';

    function AlgoritmoDeSinalDinamico() {

    }

    AlgoritmoDeSinalDinamico.prototype = {
        _reconheceuPrimeiroFrame: false,
        _reconheceuUltimoFrame: false,
        _frames: undefined,
        _sinalId: -1,

        setSinalId: function(sinalId) {
            this._sinalId = sinalId;
        },

        reconhecer: function(frame) {
            if (!this._reconheceuPrimeiroFrame) {
                this._frames = [];
                this._reconhecerPrimeiroFrame(frame);
            } else if (!this._reconheceuUltimoFrame) {
                this._frames.push(frame);
                this._reconhecerUltimoFrame(frame);
            }
        },

        _reconhecerPrimeiroFrame: function(frame) {
            Signa.Hubs
                .sinaisDinamicos()
                .reconhecerPrimeiroFrame([frame])
                .then(function(sinalReconhecidoId) {
                    if (sinalReconhecidoId == this._sinalId) {
                        console.log('RECONHECEU PRIMEIRO FRAME');
                        this._reconheceuPrimeiroFrame = true;
                        this._frames.push(frame);
                    }
                }.bind(this));
        },

        _reconhecerUltimoFrame: function(frame) {
            Signa.Hubs
                .sinaisDinamicos()
                .reconhecerUltimoFrame([frame])
                .then(function(sinalReconhecidoId) {
                    if (sinalReconhecidoId == this._sinalId) {
                        console.log('RECONHECEU ÚLTIMO FRAME');
                        this._reconheceuUltimoFrame = true;
                        this._reconhecerSinal();
                    }
                }.bind(this));
        },

        _reconhecerSinal: function() {
            Signa.Hubs
                .sinaisDinamicos()
                .reconhecer(this._frames)
                .then(function(sinalReconhecidoId) {
                    if (sinalReconhecidoId == this._sinalId) {
                        console.log('SUCESSO');
                    }
                }.bind(this));
        }
    };

    Signa.reconhecimento.AlgoritmoDeSinalDinamico = AlgoritmoDeSinalDinamico;
})(window, window.Signa);
