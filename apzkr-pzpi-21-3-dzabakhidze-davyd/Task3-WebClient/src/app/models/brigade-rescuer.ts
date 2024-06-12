import {Victim} from "./victim";
import {BaseEntity} from "./base-entity";
import {Contact} from "./contact";
import {Brigade} from "./brigade";
import {Action} from "./action";

export interface BrigadeRescuer extends BaseEntity {
  contactId: string;
  contact: Contact;
  position: string;
  specialization: string;
  brigadeId: string;
  brigade: Brigade;
  actions: Action[];
  victims: Victim[];
}
