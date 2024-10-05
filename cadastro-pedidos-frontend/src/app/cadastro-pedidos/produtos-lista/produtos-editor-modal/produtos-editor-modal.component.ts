import {Component, Inject} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import { ProdutoInput, ProdutoOutput, ProdutoService } from '../../../api';

@Component({
  selector: 'app-produtos-editor-modal',
  templateUrl: './produtos-editor-modal.component.html',
  styleUrls: ['./produtos-editor-modal.component.scss']
})
export class ProdutosEditorModalComponent {
  public form!: FormGroup;

  private isCreating = false;

  public get canSave(): boolean {
    return this.form.valid && this.form.dirty;
  }

  constructor(private formBuilder: FormBuilder, private matDialogRef: MatDialogRef<ProdutosEditorModalComponent>,
              private service: ProdutoService, @Inject(MAT_DIALOG_DATA) private produto: ProdutoOutput) {
    this.isCreating = !produto;
    this.createForm();
  }

  public closeModal(): void {
    this.matDialogRef.close();
  }

  public save(): void {
    const input: ProdutoInput = this.form.getRawValue();

    const action = this.isCreating ? this.service.produtosPost(input) : this.service.produtosIdPut(input.id, input);

    action.subscribe(() => {
      this.matDialogRef.close();
    });
  }

  private createForm(): void {
    if (this.isCreating) {
      this.produto = {} as any;
    }

    this.form = this.formBuilder.group({
      id: [this.produto.id],
      nomeProduto: [this.produto.nomeProduto, [Validators.required]],
      valor: [this.produto.valor, [Validators.required, Validators.min(0)]],
    });
  }
}
