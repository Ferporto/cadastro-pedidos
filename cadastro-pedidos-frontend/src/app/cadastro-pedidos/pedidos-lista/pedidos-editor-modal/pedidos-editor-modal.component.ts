import {Component, Inject} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import { PedidoInput, PedidoOutput, PedidoService } from '../../../api';

@Component({
  selector: 'app-pedidos-editor-modal',
  templateUrl: './pedidos-editor-modal.component.html',
  styleUrls: ['./pedidos-editor-modal.component.scss']
})
export class PedidosEditorModalComponent {
  public form!: FormGroup;

  private isCreating = false;

  public get canSave(): boolean {
    return this.form.valid && this.form.dirty;
  }

  constructor(private formBuilder: FormBuilder, private matDialogRef: MatDialogRef<PedidosEditorModalComponent>,
              private service: PedidoService, @Inject(MAT_DIALOG_DATA) private pedido: PedidoOutput) {
    this.isCreating = !pedido;
    this.createForm();
  }

  public closeModal(): void {
    this.matDialogRef.close();
  }

  public save(): void {
    const input: PedidoInput = this.form.getRawValue();

    const action = this.isCreating ? this.service.pedidosPost(input): this.service.pedidosIdPut(input.id, input);

    action.subscribe(() => {
      this.matDialogRef.close();
    });
  }

  private createForm(): void {
    if (this.isCreating) {
      this.pedido = {} as any;
    }

    this.form = this.formBuilder.group({
      id: [this.pedido.id],
      nomeCliente: [this.pedido.nomeCliente, [Validators.required]],
      emailCliente: [this.pedido.emailCliente, [Validators.required, Validators.email]],
      pago: [this.pedido.pago ?? false, [Validators.required]],
    });
  }
}
