Preuzmite i buildajte projekt
Pristupanje API endpointovima

Nakon uspješnog pokretanja aplikacije, možete pristupiti endpointovima putem sljedećih ruta:
Dohvat liste proizvoda:
GET http://localhost:5000/api/products

Dohvat detalja jednog proizvoda:
GET http://localhost:5000/api/products/{productId}

Filtriranje proizvoda po kategoriji i cijeni:
GET http://localhost:5000/api/products/filter?category={category}&minPrice={minPrice}&maxPrice={maxPrice}
Primjer korištenja: GET http://localhost:5000/api/products/filter?category=electronics&minPrice=100&maxPrice=500


Pretraga proizvoda po nazivu:
GET http://localhost:5000/api/products/search?query={query}
Primjer korištenja: GET http://localhost:5000/api/products/search?query=laptop
