<h1>StarPing</h1>
<p>
  StarPing to aplikacja webowa dostawcy internetu ze sklepem z ofertami subskrypcji i urządzeniami sieciowymi zbudowana według wzorca <strong>MVC</strong> (Model-View-Controller). Umożliwia przeglądanie oferty, składanie zamówień oraz zarządzanie koszykiem.
</p>

<p>
  Projekt składa się z trzech warstw aplikacji:
</p>
<ul>
  <li><strong>Witryna</strong> - Stanowi aplikację internetową; wysyła zapytania do API, przetwarza otrzymane dane oraz prezentuje widoki użytkownikowi.</li>
  <li><strong>Model danych</strong> - Zawiera zbiór modeli oraz kontekst bazy danych.</li>
  <li><strong>API</strong> - Zapewnia dostęp do funkcji i danych; umożliwia pobieranie lub modyfikowanie danych z bazy.</li>
</ul>

<h2>Technologie</h2>
<ul>
  <li>ASP.NET Core MVC</li>
  <li>Entity Framework Core</li>
  <li>Razor</li>
  <li>REST API (JSON)</li>
</ul>

<h2>Strona WWW</h2>
<p>
  Do prezentacji widoków użyto <strong>Razor</strong> - wygodnego połączenia HTML i C#, które pozwala mi w wygodny sposób korzystać z funkcji kontrolera lub przekierować do innego. Robię to używając atrybutów ASP oraz <code>Url.Action()</code>, w zależności od potrzeb.
  Kontrolery witryny pobierają dane z serwisów i wywołują odpowiednie widoki do zaprezentowania.
  Serwisy odpowiadają za wysyłanie zapytań do API, odbieranie otrzymanych danych i deserializowanie ich.
</p>

<h2>Warstwa modelu</h2>
<p>
  Jest biblioteką klas - zawiera kontekst bazy danych oraz obiekty stanowiące jej strukturę i relacje między nimi. Aplikacja została stworzona z wykorzystaniem podejścia <strong>Code First</strong>, co umożliwia ponowne odtworzenie bazy danych.
</p>

<h2>API</h2>
<p>
  Stanowi źródło danych i obiekt zapytań dla strony internetowej. API przyjmuje żądania za pośrednictwem adresu URL z ewentualnymi parametrami lub zserializowanymi obiektami, następnie, korzystając z serwisów, pobiera lub modyfikuje dane oraz zwraca je z kodem odpowiedzi do klienta.
  Do komunikacji z bazą danych używam <strong>Entity Framework Core</strong>.
</p>

<h2>Licencja</h2>
<p>
  Ten projekt jest udostępniony na licencji <strong>MIT</strong>.
</p>
