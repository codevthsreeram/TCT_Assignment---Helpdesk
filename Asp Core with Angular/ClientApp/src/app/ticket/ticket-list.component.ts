import { Component } from "@angular/core";
import { FormControl, FormGroup } from "@angular/forms";
import { Ticket } from "../Models/ticket";
import { TicketService } from "../Services/ticket.service";

@Component({
  selector: 'ticket',
  templateUrl: './ticket-list.component.html'
})

export class TicketListComponent {
  public tickets: Ticket[];
  mode: string = "Create";
  ticketDetail = new Ticket();
  ticketForm = new FormGroup({
    title: new FormControl(''),
    description: new FormControl(''),
    type: new FormControl(''),
    status: new FormControl('')
  })
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
  view() {
    this.mode = "View";
  }
  add() {
    this.mode = "Create";
    this.ticketDetail = new Ticket();
  }
  edit(ticketDetail: Ticket) {
    this.mode = "Edit";
    this.ticketDetail = ticketDetail;
  }
  saveTicket() {
    if (this.ticketDetail.id) {
      this.ticketService.update(this.ticketDetail.id, this.ticketDetail).subscribe(response => {
        this.loadData();
      });
    }
    else {
      this.ticketService.create(this.ticketDetail).subscribe(response => {
        this.loadData();
      });
    }
  }
  delete(id: any) {
    this.ticketService.delete(id).subscribe(response => {
      this.loadData();
    });
  }
}
