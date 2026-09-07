export interface Relation {
  code: number;
  city: string;
  contactPerson: string;
  email: string;
  id: string;        
  name: string;
  postalCode: string;
}

export interface RelationsResponse {
  results: Relation[];
}
