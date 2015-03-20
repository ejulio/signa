;(function(window, Signa, undefined) {
    'use strict';

    function AlgoritmoDeSinalDinamico() {

    }

    AlgoritmoDeSinalDinamico.prototype = {
        _reconheceuPrimeiroFrame: false,
        _reconheceuUltimoFrame: false,
        _frames: undefined,
        _sinalId: -1,
        _promiseReconhecerPrimeiroFrame: undefined,
        _framesIgnorados: 0,

        setSinalId: function(sinalId) {
            this._sinalId = sinalId;
        },

        reconhecer: function(frame) {
            if (!this._reconheceuPrimeiroFrame) {
                this._frames = [];
                return this._reconhecerPrimeiroFrame(frame);    
            }
            
            return this._reconhecerUltimoFrame(frame);
        },

        _reconhecerPrimeiroFrame: function(frame) {
            if (this._promiseReconhecerPrimeiroFrame) {
                this._framesIgnorados++;
            }

            this._promiseReconhecerPrimeiroFrame = Signa.Hubs
                .sinaisDinamicos()
                .reconhecerPrimeiroFrame([frame])
                .then(function(sinalReconhecidoId) {
                    this._promiseReconhecerPrimeiroFrame = undefined;
                    this._framesIgnorados = 0;
                    if (sinalReconhecidoId === this._sinalId) {
                        this._reconheceuPrimeiroFrame = true;
                        this._frames.push(frame);
                    }
                    return false;
                }.bind(this));

            return this._promiseReconhecerPrimeiroFrame;
        },

        _reconhecerUltimoFrame: function(frame) {
            return Signa.Hubs
                .sinaisDinamicos()
                .reconhecerUltimoFrame([frame])
                .then(function(sinalReconhecidoId) {
                    this._frames.push(frame);
                    if (sinalReconhecidoId == this._sinalId) {
                        this._reconheceuUltimoFrame = true;
                        this._reconhecerSinal();
                    } else if (this._frames.length == 50) {
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
                    this._reconheceuPrimeiroFrame = false;
                    if (sinalReconhecidoId == this._sinalId) {
                        console.log('SUCESSO');
                        return true;
                    }
                }.bind(this));
        }
    };

    Signa.reconhecimento.AlgoritmoDeSinalDinamico = AlgoritmoDeSinalDinamico;
})(window, window.Signa);
