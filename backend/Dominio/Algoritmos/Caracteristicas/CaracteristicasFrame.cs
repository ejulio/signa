using Dominio.Sinais.Frames;
using Dominio.Sinais.Maos;
using Dominio.Util;
using Dominio.Util.Matematica;
using System.Collections.Generic;
using System.Linq;

namespace Dominio.Algoritmos.Caracteristicas
{
    public abstract class CaracteristicasFrame
    {
        protected double[] CaracteristicasDoFrame(Frame frame)
        {
            return CaracteristicasDaMao(frame.MaoEsquerda)
                .Concat(CaracteristicasDaMao(frame.MaoDireita))
                .ToArray();    
        }

        private IEnumerable<double> CaracteristicasDaMao(Mao mao)
        {
            return mao.VetorNormalDaPalma.Normalizado()
                .Concat(mao.Direcao.Normalizado())
                .Concat(AngulosEntreDedos(mao))
                .Concat(DirecaoDosDedos(mao));
        }

        private IEnumerable<double> AngulosEntreDedos(Mao mao)
        {
            var angulos = new double[mao.Dedos.Length - 1];
            
            for (int i = 0; i < angulos.Length; i++)
            {
                var dedoAtual = mao.Dedos[i];
                var proximoDedo = mao.Dedos[i + 1];
                var posicaoDedoAtual = PosicaoDoDedoProjetadaNoPlanoDaPalma(dedoAtual, mao);
                var posicaoProximoDedo = PosicaoDoDedoProjetadaNoPlanoDaPalma(proximoDedo, mao);
                angulos[i] = posicaoDedoAtual.AnguloAte(posicaoProximoDedo);
            }

            return angulos.Normalizado();
        }

        private double[] PosicaoDoDedoProjetadaNoPlanoDaPalma(Dedo dedo, Mao mao)
        {
            return dedo.PosicaoDaPonta
                .ProjetadoNoPlano(mao.VetorNormalDaPalma);
        }

        private IEnumerable<double> DirecaoDosDedos(Mao mao)
        {
            return mao.Dedos
                .Select(d => d.Direcao.Normalizado())
                .ToArray()
                .Concatenar();
        }

        //private IEnumerable<double> ElevacaoDosDedos(Mao mao)
        //{
        //    var elevacoes = new double[mao.Dedos.Length];

        //    for (int i = 0; i < elevacoes.Length; i++)
        //    {
        //        var posicaoDoDedoProjetada = mao.Dedos[i].PosicaoDaPonta.ProjetadoNoPlano(mao.VetorNormalDaPalma);
        //        var diferencaNaPosicaoDeDedo = mao.Dedos[i].PosicaoDaPonta.Subtrair(posicaoDoDedoProjetada);
        //        var sinal = Math.Sign(diferencaNaPosicaoDeDedo.ProdutoCom(mao.VetorNormalDaPalma));

        //        elevacoes[i] = sinal * diferencaNaPosicaoDeDedo.Magnitude();
        //    }

        //    return elevacoes.Normalizado();
        //}

        //private double[] AngulosEntreDedosEPalmaDaMao(Mao mao)
        //{
        //    double[][] angulos = new double[mao.Dedos.Length][];

        //    for (int i = 0; i < angulos.Length; i++)
        //    {
        //        var posicaoDaPontaDoDedo = mao.Dedos[i].PosicaoDaPonta;
        //        var posicaoDaPalma = mao.PosicaoDaPalma;

        //        angulos[i] = new[]
        //        {
        //            posicaoDaPontaDoDedo.ProjetadoEmXY().AnguloAte(posicaoDaPalma.ProjetadoEmXY()),
        //            posicaoDaPontaDoDedo.ProjetadoEmXZ().AnguloAte(posicaoDaPalma.ProjetadoEmXZ()),
        //            posicaoDaPontaDoDedo.ProjetadoEmYZ().AnguloAte(posicaoDaPalma.ProjetadoEmYZ())
        //        }.Normalizado();
        //    }
        //    return angulos.Concatenar().ToArray();
        //}

        //private double[] AngulosEntreDedosEPalmaDaMao(Mao mao)
        //{
        //    double[] angulos = new double[mao.Dedos.Length];

        //    for (int i = 0; i < angulos.Length; i++)
        //    {
        //        var posicaoDaPontaDoDedo = mao.Dedos[i].PosicaoDaPonta;
        //        var posicaoDaPalma = mao.PosicaoDaPalma;

        //        angulos[i] = posicaoDaPalma.AnguloAte(posicaoDaPontaDoDedo);
        //    }
        //    return angulos.Normalizado();
        //}

        //private double[] AngulosEntreDedos(Mao mao)
        //{
        //    double[][] angulos = new double[mao.Dedos.Length - 1][];
        //    var posicaoPalma = mao.PosicaoDaPalma.MultiplicarPor(-1);

        //    for (int i = 0; i < angulos.Length; i++)
        //    {
        //        var posicaoDaPontaDoDedo1 = mao.Dedos[i].PosicaoDaPonta.SomarCom(posicaoPalma);
        //        var posicaoDaPontaDoDedo2 = mao.Dedos[i + 1].PosicaoDaPonta.SomarCom(posicaoPalma);

        //        angulos[i] = new[]
        //        {
        //            posicaoDaPontaDoDedo1.ProjetadoEmXY().AnguloAte(posicaoDaPontaDoDedo2.ProjetadoEmXY()),
        //            posicaoDaPontaDoDedo1.ProjetadoEmXZ().AnguloAte(posicaoDaPontaDoDedo2.ProjetadoEmXZ()),
        //            posicaoDaPontaDoDedo1.ProjetadoEmYZ().AnguloAte(posicaoDaPontaDoDedo2.ProjetadoEmYZ())
        //        }.Normalizado();
        //    }
        //    return angulos.Concatenar().ToArray();
        //}

        //private double[] AngulosEntreDedos(Mao mao)
        //{
        //    double[] angulos = new double[mao.Dedos.Length - 1];
        //    var posicaoPalma = mao.PosicaoDaPalma.MultiplicarPor(-1);

        //    for (int i = 0; i < angulos.Length; i++)
        //    {
        //        var posicaoDaPontaDoDedo1 = mao.Dedos[i].PosicaoDaPonta.SomarCom(posicaoPalma);
        //        var posicaoDaPontaDoDedo2 = mao.Dedos[i + 1].PosicaoDaPonta.SomarCom(posicaoPalma);

        //        angulos[i] = posicaoDaPontaDoDedo1.AnguloAte(posicaoDaPontaDoDedo2);
        //    }
        //    return angulos.Normalizado();
        //}
    }
}