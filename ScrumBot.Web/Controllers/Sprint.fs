namespace ScrumBot.Web.Models

open System

type TeamMember = { Name: string; Coefficient: float; }
type Workitem = { id: string; Name: string; Spent: float; Remaining: float; Original: float }
type UserStory = { id: string; Name: string; Workitems: list<Workitem> }
type Sprint = { id : string; Name : string; TeamName : string; StartDate: DateTime; EndDate: DateTime; Duration: int; TeamMembers: list<TeamMember>; UserStories: list<UserStory> }

