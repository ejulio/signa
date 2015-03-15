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
                return this._reconhecerPrimeiroFrame(frame);
            } else if (!this._reconheceuUltimoFrame) {
                this._frames.push(frame);
                return this._reconhecerUltimoFrame(frame);
            }
        },

        _reconhecerPrimeiroFrame: function(frame) {
            return Signa.Hubs
                .sinaisDinamicos()
                .reconhecerPrimeiroFrame([frame])
                .then(function(sinalReconhecidoId) {
                    if (sinalReconhecidoId == this._sinalId) {
                        console.log('RECONHECEU PRIMEIRO FRAME');
                        this._reconheceuPrimeiroFrame = true;
                        this._frames.push(frame);
                    }
                    return false;
                }.bind(this));
        },

        _reconhecerUltimoFrame: function(frame) {
            return Signa.Hubs
                .sinaisDinamicos()
                .reconhecerUltimoFrame([frame])
                .then(function(sinalReconhecidoId) {
                    if (sinalReconhecidoId == this._sinalId) {
                        console.log('RECONHECEU ÚLTIMO FRAME');
                        this._reconheceuUltimoFrame = true;
                        this._reconhecerSinal();
                    }
                    return false;
                }.bind(this));
        },

        _reconhecerSinal: function() {
            return Signa.Hubs
                .sinaisDinamicos()
                .reconhecer(this._frames)
                .then(function(sinalReconhecidoId) {
                    if (sinalReconhecidoId == this._sinalId) {
                        console.log('SUCESSO');
                        return true;
                    }
                }.bind(this));
        }
    };

    Signa.reconhecimento.AlgoritmoDeSinalDinamico = AlgoritmoDeSinalDinamico;
})(window, window.Signa);
