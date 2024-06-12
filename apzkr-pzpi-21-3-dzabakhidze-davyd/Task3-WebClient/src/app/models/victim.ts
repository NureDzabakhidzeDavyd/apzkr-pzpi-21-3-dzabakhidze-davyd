import {Action} from "./action";
import {BaseEntity} from "./base-entity";
import {Contact} from "./contact";
import {BrigadeRescuer} from "./brigade-rescuer";
import {Diagnosis} from "./diagnosis";

export interface Victim extends BaseEntity {
  contactId: string;
  contact: Contact;
  brigadeRescuerId: string;
  brigadeRescuer: BrigadeRescuer;
  diagnoses: Diagnosis[];
  actions: Action[];
}
