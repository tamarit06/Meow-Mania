public class Celda
{
    public enum TipoCelda
    {
        Camino,
        Pared,
        Trampa,
        Obstaculo
    }

    public TipoCelda Tipo { get; set; }

    public Celda()
    {
        Tipo = TipoCelda.Pared;
         
    }

    public void ConvertirEnCamino()
    {
        Tipo = TipoCelda.Camino;
    }

    public void ConvertirEnPared()
    {
        Tipo = TipoCelda.Pared;
    }

    public void ConvertirEnTrampa()
    {
        Tipo = TipoCelda.Trampa;
    }

    public void ConvertirEnObstaculo()
    {
        Tipo = TipoCelda.Obstaculo;
    }
}
