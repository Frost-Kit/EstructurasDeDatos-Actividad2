
namespace PanaderiaBendt.Models.classes.DateStructures;

// Se esta usando esta para las tres tipos de clases de listas enlazadas
public interface IListaEnlazada<T>
{
    bool EsVacia();
    void AgregarInicio(T elemento);
    void AgregarEn(int indice, T elemento);
    void AgregarFin(T elemento);
    void EliminarInicio();
    void EliminarEn(int indice);
    void EliminarFin();
    void EliminarValor(T element); // Para nosotros
    int BuscarElemento(T elemento);
    int Cantidad();
    void VaciarLista();
    void MostrarDatos();
    void MostrarDatosInverso();
}
