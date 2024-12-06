# ApInsta

ApInsta é uma API de rede social construída em **.NET 9**, seguindo os princípios de **DDD (Domain-Driven Design)**, **Clean Architecture** e **Clean Code**. Esta versão inicial inclui funcionalidades básicas para autenticação de usuários e organização de dados.

---

## Funcionalidades

- Registro de novos usuários.
- Autenticação com JWT (JSON Web Tokens).
- Gerenciamento de usuários via repositórios.
- Testes automatizados com xUnit.
- Estrutura organizada com **DDD** e **Camadas**:
  - **Domain**: Contém as entidades principais do sistema.
  - **Application**: Implementação de serviços e casos de uso.
  - **Infrastructure**: Persistência de dados com **Entity Framework Core**.
  - **API**: Endpoints para interação com o sistema.

---

## Tecnologias Utilizadas

- **C#** com **.NET 9**
- **Entity Framework Core** para persistência.
- **SQL Server** como banco de dados.
- **JWT** para autenticação.
- **FluentValidation** para validação de dados.
- **Swagger** para documentação da API.

---

## Pré-requisitos

Antes de começar, você precisará ter instalado:

- [SDK do .NET 9](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads)
- Um editor como [Visual Studio Code](https://code.visualstudio.com/) ou [Visual Studio](https://visualstudio.microsoft.com/).

---

## Configuração do Projeto

1. Clone o repositório:

   ```bash
		git clone https://github.com/VinyRN/ApInsta
		cd ApInsta
   ```

2. Configure a string de conexão no arquivo appsettings.json:

```json
"ConnectionStrings": {
	"DefaultConnection": "Server=localhost;Database=BASE;User Id=sa;Password=Bkur6etc@10;TrustServerCertificate=True;"
  }
```

3. Restaure as dependências:

	```bash
		dotnet restore
	```
4. Crie o banco de dados e aplique as migrações:

	```bash
		cd src/ApInsta.Infrastructure/
		dotnet ef migrations add InitialCreate 
		dotnet ef database update 
	```
	
5. Execute a aplicação:
    ```bash
		cd ..
		cd ApInsta.API/
		dotnet run 
	```
6. Acesse o Swagger para testar os endpoints:

## Testes Automatizados

  Os testes são realizados com xUnit, usando Moq para mocks e FluentAssertions para validações expressivas.

## Rodar os Testes

  Para executar todos os testes, use o comando, no projeto "ApInsta.Tests":
```bash
  dotnet test
```

## Resultado Esperado
```bash
  ApInsta\tests\ApInsta.Tests> dotnet test
  Restauração concluída (0,3s)
    ApInsta.Domain êxito (0,0s) → \ApInsta\src\ApInsta.Domain\bin\Debug\net9.0\ApInsta.Domain.dll
    ApInsta.Service êxito (0,2s) → \ApInsta\src\ApInsta.Service\bin\Debug\net9.0\ApInsta.Service.dll
    ApInsta.Tests êxito (0,2s) → bin\Debug\net9.0\ApInsta.Tests.dll
  [xUnit.net 00:00:00.00] xUnit.net VSTest Adapter v2.8.2+699d445a1a (64-bit .NET 9.0.0)
  [xUnit.net 00:00:00.06]   Discovering: ApInsta.Tests
  [xUnit.net 00:00:00.09]   Discovered:  ApInsta.Tests
  [xUnit.net 00:00:00.09]   Starting:    ApInsta.Tests
  [xUnit.net 00:00:01.17]   Finished:    ApInsta.Tests
    ApInsta.Tests teste êxito (1,8s)

  Resumo do teste: total: 4; falhou: 0; bem-sucedido: 4; ignorado: 0; duração: 1,8s
  Construir êxito em 3,0s
```

## Principais Testes

  Login Válido: Testa se um token JWT é gerado corretamente para credenciais válidas.

  Login Inválido: Testa se o login falha para credenciais incorretas.

  Registro Válido: Testa o registro de um novo usuário.
  
  E-mail já existente: Testa se o sistema lança uma exceção ao tentar registrar um e-mail já existente.

## Estrutura do Projeto

    src/
      ├── ApInsta.API           # Camada de apresentação (endpoints)
      ├── ApInsta.Application   # Casos de uso e validações
      ├── ApInsta.Core          # Contratos e interfaces gerais
      ├── ApInsta.Domain        # Entidades e lógica de domínio
      ├── ApInsta.Infrastructure# Persistência de dados e repositórios
      └── ApInsta.Service       # Serviços auxiliares (futuro)
    └── tests/
      └── ApInsta.Tests/          # Testes Automatizados

## Endpoints Principais

### Autenticação
    POST /auth/register
    Registro de um novo usuário.
    Body (JSON):
	
```json
{
	"name": "fulano",
	"email": "fulano@ciclano.com",
	"password": "password123"
}
```
	
    POST /auth/login
    Autenticação de usuário.
    Body (JSON):
	
```json
{
	"login": "johndoe@example.com",
	"password": "password123"
}
```


