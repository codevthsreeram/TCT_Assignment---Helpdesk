import { Component } from "@angular/core";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { Ticket } from "../Models/ticket";
import { TicketService } from "../Services/ticket.service";

@Component({
  selector: 'ticket',
  templateUrl: './ticket-list.component.html'
})

export class TicketListComponent {
  modalTitle: string;
  alertMessage: string;
  public tickets: Ticket[];
  mode: string = "Create";
  formData = new Ticket();
  ticketForm = new FormGroup({
    title: new FormControl('', Validators.required),
    description: new FormControl(''),
    type: new FormControl(''),
    status: new FormControl('')
  })

  get title() {
    return this.ticketForm.get('title');
  }
  get description() {
    return this.ticketForm.get('description');
  }
  get type() {
    return this.ticketForm.get('type');
  }
  get status() {
    return this.ticketForm.get('status');
  }

  constructor(private ticketService: TicketService) {

  }
  ngOnInit(): void {
    this.loadData();
  }
  loadData() {
    this.ticketService.getAll()
      .subscribe(
        response => {
          this.tickets = response as Ticket[];
        },
        error => {
          console.log(error);
        }
      )
  }
  viewTicket(ticketDetail: Ticket) {
    this.mode = "View";
    this.modalTitle = "View Ticket";
    this.formData = ticketDetail;
    this.alertMessage = "";
  }
  addTicket() {
    this.mode = "Create";
    this.modalTitle = "Add Ticket";
    this.formData = new Ticket();    
    this.alertMessage = "";
    this.ticketForm.reset();
  }
  editTicket(ticketDetail: Ticket) {
    this.mode = "Edit";
    this.modalTitle = "Edit Ticket";
    this.formData = ticketDetail;
    this.alertMessage = "";
  }
  saveTicket() {
    if (this.formData.id) {
      this.ticketService.update(this.formData.id, this.formData).subscribe(response => {
        this.loadData();
        this.alertMessage = "Ticket updated successfully..!";
      });
    }
    else {
      this.ticketService.create(this.formData).subscribe(response => {
        this.loadData();
        this.alertMessage = "Ticket created successfully..!";
      });
    }
    this.formData = new Ticket();
    this.ticketForm.reset();
  }
  deleteTicket(id: any) {
    this.ticketService.delete(id).subscribe(response => {
      this.loadData();
      this.alertMessage = "Ticket deleted successfully..!";
    });
  }
}
