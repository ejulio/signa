var FrameBuilder = require('../../builders/FrameBuilder.js'),
    MaoBuilder = require('../../builders/MaoBuilder.js'),
    DedoBuilder = require('../../builders/DedoBuilder.js');

global.THREE = require('three');
require('../../../src/signa/Signa.js');
require('../../../src/signa/reconhecimento/InformacoesDoFrame.js');

describe('InformacoesDoFrame', function()
{
    var DIRECAO_MAO_ESQUERDA = [1, 2, 3],
        VETOR_NORMAL_PALMA_MAO_ESQUERDA = [0, 0, 1],
        DIRECAO_MAO_DIREITA = [4, 5, 6],
        VETOR_NORMAL_PALMA_MAO_DIREITA = [1, 1, 0];

    var informacoesDoFrame;
    beforeEach(function()
    {
        informacoesDoFrame = new Signa.reconhecimento.InformacoesDoFrame();
    });

    it('retornando dados do frame apenas com a mão esquerda', function()
    {
        var maoEsquerda = dadaUmaMaoEsquerda();
        var frame = dadoUmFrameComMaos(maoEsquerda, null);

        var frameDaAmostra = informacoesDoFrame.extrairParaAmostra(frame);

        expect(frameDaAmostra.MaoDireita).toBeNull();
        deveRetornarUmFrameDaAmostraComDadosDoLeapFrame(frameDaAmostra.MaoEsquerda, maoEsquerda);
    });

    it('retornando dados do frame apenas com a mão direita', function()
    {
        var maoDireita = dadaUmaMaoDireita();
        var frame = dadoUmFrameComMaos(null, maoDireita);

        var frameDaAmostra = informacoesDoFrame.extrairParaAmostra(frame);

        expect(frameDaAmostra.MaoEsquerda).toBeNull();
        deveRetornarUmFrameDaAmostraComDadosDoLeapFrame(frameDaAmostra.MaoDireita, maoDireita);
    });

    it('retornando dados do frame apenas com as duas mãos', function()
    {
        var maoEsquerda = dadaUmaMaoEsquerda();
        var maoDireita = dadaUmaMaoDireita();
        var frame = dadoUmFrameComMaos(maoEsquerda, maoDireita);

        var frameDaAmostra = informacoesDoFrame.extrairParaAmostra(frame);

        deveRetornarUmFrameDaAmostraComDadosDoLeapFrame(frameDaAmostra.MaoEsquerda, maoEsquerda);
        deveRetornarUmFrameDaAmostraComDadosDoLeapFrame(frameDaAmostra.MaoDireita, maoDireita);
    });

    function dadaUmaMaoEsquerda()
    {
        var dedos = [
            DedoBuilder.dedao(),
            DedoBuilder.indicador(),
            DedoBuilder.meio(),
            DedoBuilder.anelar(),
            DedoBuilder.mindinho()
        ];

        return new MaoBuilder()
            .comDirecao(DIRECAO_MAO_ESQUERDA)
            .comVetorNormalDaPalma(VETOR_NORMAL_PALMA_MAO_ESQUERDA)
            .comDedos(dedos)
            .construir();
    }

    function dadaUmaMaoDireita()
    {
        var dedos = [
            DedoBuilder.dedao(),
            DedoBuilder.indicador(),
            DedoBuilder.meio(),
            DedoBuilder.anelar(),
            DedoBuilder.mindinho()
        ];

        return new MaoBuilder()
            .comDirecao(DIRECAO_MAO_DIREITA)
            .comVetorNormalDaPalma(VETOR_NORMAL_PALMA_MAO_DIREITA)
            .comDedos(dedos)
            .construir();
    }

    function dadoUmFrameComMaos(maoEsquerda, maoDireita)
    {
        return new FrameBuilder()
            .comMaoEsquerda(maoEsquerda)
            .comMaoDireita(maoDireita)
            .construir();
    }

    function deveRetornarUmFrameDaAmostraComDadosDoLeapFrame(frameDaAmostra, mao)
    {
        expect(frameDaAmostra).not.toBeNull();
        expect(frameDaAmostra.VetorNormalDaPalma).toBe(mao.palmNormal);
        expect(frameDaAmostra.Direcao).toBe(mao.direction);
        
        var dedos = frameDaAmostra.Dedos;        
        for (var i = 0; i < dedos.length; i++)
        {
            expect(dedos[i].Tipo).toBe(mao.fingers[i].type);
            expect(dedos[i].Direcao).toBe(mao.fingers[i].direction);
        }
    }
});
