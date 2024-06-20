# Application Form API
This is a REST API designed to handle application forms using Azure Cosmos DB. Users have the ability to tailor forms to their needs, each comprising a title, description, and a set of questions. Questions can vary in type, including Number, Date/time, Multichoice, Dropdown, or Paragraph formats.

The API functionalities include:
- Create a form
- Create a single question
- Update a single question
- View question by question type
- Submit form responses

## Architecture of API
The API makes use of a layered architecture.

### Infrastructure layer
This layer manages the functional implementation and database abstraction. It acts as a bridge to the database layer, it includes:

- **Services**: Acts as an interface between controllers and database operations, facilitating user interactions.
- **Repositories**: Implements data persistence and querying functionalities for the database.
- **CosmosDB**: Manages connections to the Cosmos DB, ensuring seamless database interactions.
- **Middlewares** : Implements custom middleware functionalities.
- **Extensions** : Manages model and response result extensions.
- **Utility**: Implements various other functionalities.
- **Validations**: Handles custom validation processes.

### Model layer
This oversees the operation of the application and includes Data Transfer Objects (DTOs) used for managing data transmission.

### Controller layer
This comprises endpoints that clients interact with, such as:
- **Application form**: Responsible for managing application form endpoints.
- **Question**: Handles endpoints related to questions.

## Packages
- **Cosmos db**:
    - Azure.Identity
    - Microsoft.Azure.Cosmos
- **Mapping** 
    - Mapster
- **API documenation**:
    - Swashbuckle.AspNetCore     

## **How to use**
##### 1. *Create a new application form*
*Endpoint*: api/application-forms

*HTTP verb*: POST

*Content type* : application/json

*Responses* : [201,400,500] 

*Response type* : application/json

```
 Payload:
   {
       "title": "First application form",
       "description": "Creating first application form",
       "questions":
       [
            {
                "questionType": "Paragraph",
                "content":"FirstName"
                "questionNumber": 1,
                "questionGroup":"PersonalInformation"
            },
             {
                "questionType": "DropDown",
                "content":"Select from any"
                "questionNumber": 2,
                "choices":
                [
                    {
                        "content":"C#",
                        "choiceNumber": 1
                    },
                    {
                        
                        "content":"Java",
                        "choiceNumber": 2
                    }
                    
                ]
                "questionGroup":"PersonalInformation"
            } 
       ]
    }
 ```

##### 2. *Add an existing question to a form*
*Endpoint*: api/**{{form-id}}**/questions

*HTTP verb*: POST

*Content type*: application/json

*Responses*: [201,400,500] 

*Response type*: application/json

```
Payload:
    {
       "questions":
       [
            {
                "questionType": "Paragraph",
                "content":"FirstName"
                "questionNumber": 1,
                "questionGroup":"PersonalInformation"
            },
             {
                "questionType": "DropDown",
                "content":"Select from any"
                "questionNumber": 2,
                "choices":
                [
                    {
                        "content":"C#",
                        "choiceNumber": 1
                    },
                    {
                        
                        "content":"Java",
                        "choiceNumber": 2
                    }
                    
                ]
                "questionGroup":"PersonalInformation"
            } 
       ]
    }
```    

#### 3.1 *Fetch questions by question type in a form*
###### *e.g Paragraph, Yes/no, Number and Date/time*
*Endpoint*: api/**{{form-id}}**/questions?questionType=Paragraph

*HTTP verb*: GET

*Content type*: application/json

*Responses*: [200,404,500] 

*Response type*: application/json

```
Response payload:
    {
        "questionId":"12345"
        "content":"First name"
    }
```

##### 3.2 *Fetch questions by drop down question type in a form*
*Endpoint*: api/**{{form-id}}**/questions?questionType=DropDown

*HTTP verb*: GET

*Content type*: application/json

*Responses*: [200,404,500] 

*Response type*: application/json

```
Response payload:
    {
        "questionId":"12345"
        "content":"Select any",
        "choices":
        [
            {
               "content":"C#",
                "id": ""12555"
            },
            {
                        
                "content":"Java",
                "id": ""123444"
            }  
        ]
    }
```

### 3.3 *Fetch questions by multi choice question type in a form*
*Endpoint*: api/**{{form-id}}**/questions?questionType=Multichoice

*HTTP verb*: GET

*Content type*: application/json

*Responses*: [200,404,500] 

*Response type*: application/json

```
Response payload:
    {
        "questionId":"12345"
        "content":"Select any two",
        "choices":
        [
            {
               "content":"C#",
                "id": ""12555"
            },
            {
                        
                "content":"Java",
                "id": ""123444"
            },
            {
               "content":"C++",
                "id": ""12545"
            },
            {
                        
                "content":"Ruby",
                "id": ""123544"
            } 
        ],
        "maxChoices":"2"
    }
```

### 4. *Update existing question in a form*
*Endpoint*: api/**{{form-id}}**/questions/**{{question-id}}**

*HTTP verb*: PUT

*Content type*: application/json

*Responses*: [200,400,500] 

*Response type*: application/json

```
Payload:
    {
       "questions":
       [
            {
                "content":"FirstName"
            },
             {
                "questionType": "DropDown",
                "content":"Select from any"
                "choices":
                [
                    {
                        "content":"C#",
                        "choiceNumber": 1
                    },
                    {
                        
                        "content":"Java",
                        "choiceNumber": 2
                    }  
                ]
            },
            {
                "questionType:"Multichoice",
                "content":"Select 2"
                "choices":
                [
                   {
                        "content":"C#",
                        "choiceNumber": 1
                    },
                    {
                        
                        "content":"Java",
                        "choiceNumber": 2
                    },
                    {
                        "content":"Asp.net core",
                        "choiceNumber": 1
                    },
                    {
                        
                        "content":"Ruby",
                        "choiceNumber": 2
                    }
                ],
                "maxChoices": 2
            } 
       ]
    }
```

### 5. *Submit existing form*
*Endpoint*: api/application-forms/**{{form-id}}**/submit

*HTTP verb*: POST

*Content type*: application/json

*Responses*: [200,400,,404,500] 

*Response type*: application/json

###### 5.1 *Paragraph/Dropdown/DateTime/Number payload*
```
Payload:
    {
        "questionId":"12233",
        "response":"Adeola Aderibigbe"
    }
```

###### 5.2 *Multichoice payload*
```
Payload:
    {
        "questionId":"12233",
        "response":
        [
            "123",
            "123
        ]
    }
```

###### 5.3 *Yes/No payload*
```
Payload:
    {
        "questionId":"12233",
        "response": 1
    }
```