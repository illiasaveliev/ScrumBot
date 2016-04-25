namespace ScrumBot.Web.Controllers
open ScrumBot.Web.Models

open System
open System.Linq
open Microsoft.Azure.Documents
open Microsoft.Azure.Documents.Client
open Microsoft.Azure.Documents.Linq
open System.Net
open System.Net.Http
open System.Web.Http

/// Retrieves sprints.
[<RoutePrefix("api")>]
type SprintsController() =
    inherit ApiController()
        
    let client = new DocumentClient(new Uri(""), "")
        // Get or create a named database
    let getDatabase dbName = async {
        let existingDatabases = 
            client.CreateDatabaseQuery()
            |> Seq.where (fun d -> d.Id = dbName)
            |> Seq.toList

        return existingDatabases.First()
    }

    // Get or create a named collection within a named database
    let getCollection dbName collectionName = async {
        let! db = getDatabase dbName
        let existing = 
            client.CreateDocumentCollectionQuery(db.SelfLink)
            |> Seq.where (fun dc -> dc.Id = collectionName)
            |> Seq.toList

        return existing.First()
    }

    let getDocumentsByName dbName collectionName docName = async {
        let! collection = getCollection dbName collectionName
        let query = Seq.toList <| client.CreateDocumentQuery<Sprint>(collection.DocumentsLink).Where(fun d -> d.Name = docName)
        return query
    }

    let getDocumentById dbName collectionName docId = async {
        let! docCollection = getCollection dbName collectionName
        let docQuery = Seq.toList <| client.CreateDocumentQuery<Sprint>(docCollection.DocumentsLink).Where(fun d -> d.id = docId.ToString())
        return docQuery.FirstOrDefault()
    }
       
    /// Gets all sprints.
    [<Route("sprints")>]
    member x.Get() =  Async.RunSynchronously <| async { 
            let! documents = getDocumentsByName "ScrumBotDB" "Sprints" "S11"      
            return documents      
        }        
    
    /// Gets a single sprint at the specified id.
    [<Route("sprints/{id}")>]
    member x.Get(request: HttpRequestMessage, id: int) =
        Async.RunSynchronously <| async { 
            let! document = getDocumentById "ScrumBotDB" "Sprints" id    
            return document      
        }        

