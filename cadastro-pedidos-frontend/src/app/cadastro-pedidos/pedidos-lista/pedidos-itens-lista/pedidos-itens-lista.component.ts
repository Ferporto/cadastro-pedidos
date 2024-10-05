import { Component, Inject } from '@angular/core';
import { ItensPedidoInput, ItensPedidoOutput, ItensPedidoOutputPagedResultDto, ItensPedidoService, PedidoOutput, ProdutoOutput, ProdutoOutputPagedResultDto, ProdutoService } from '../../../api';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-pedidos-itens-lista',
  templateUrl: './pedidos-itens-lista.component.html',
  styleUrl: './pedidos-itens-lista.component.scss'
})
export class PedidosItensListaComponent {
  public form!: FormGroup;

  public columns: string[] = ['actions', 'nomeProduto', 'valorProduto', 'quantidade'];
  public itensPedidos: ItensPedidoOutput[] = [];

  public produtos: ProdutoOutput[] = [];

  private isCreating = false;

  public get canSave(): boolean {
    return this.form.valid && this.form.dirty;
  }

  public get podeLimparFormulario(): boolean {
    return this.form.dirty;
  }

  constructor(private formBuilder: FormBuilder, private matDialogRef: MatDialogRef<PedidosItensListaComponent>,
              private service: ItensPedidoService, @Inject(MAT_DIALOG_DATA) private pedido: PedidoOutput,
              private produtoService: ProdutoService) {
    this.isCreating =  true;
    this.getItensPedidos();
    this.createForm();
  }

  public closeModal(): void {
    this.matDialogRef.close();
  }

  public save(): void {
    const input: ItensPedidoInput = this.form.getRawValue();
    console.log(input);

    const action = this.isCreating ? this.service.pedidosIdPedidoItensPost(this.pedido.id, input): this.service.pedidosIdPedidoItensIdPut(this.pedido.id, input.id, input);

    action.subscribe(() => {
      this.form.reset({idPedido: this.pedido.id}, {emitEvent: false});
      this.getItensPedidos();
    });
  }

  public limparFormulario(): void {
    this.isCreating = true;
    this.form.reset({idPedido: this.pedido.id}, {emitEvent: false});
  }

  public delete(itensPedido: ItensPedidoOutput) {
    this.service.pedidosIdPedidoItensIdDelete(this.pedido.id, itensPedido.id).subscribe(() => {
      this.limparFormulario();
      this.getItensPedidos();
    });
  }

  public update(itensPedido: ItensPedidoOutput) {
    this.isCreating = false;
    console.log(itensPedido);
    this.form.reset(itensPedido, {emitEvent: false});
  }

  private createForm(): void {
    this.form = this.formBuilder.group({
      id: [],
      idPedido: [this.pedido.id],
      idProduto: [],
      quantidade: [null, [Validators.required]],
    });

    this.getProdutos();
  }

  private getItensPedidos(): void {
    this.service.pedidosIdPedidoItensGet(this.pedido.id).subscribe((pedidosPaged: ItensPedidoOutputPagedResultDto) => {
      this.itensPedidos = pedidosPaged.itens;
    });
  }

  private getProdutos(): void {
    this.produtoService.produtosGet().subscribe((produtosPaged: ProdutoOutputPagedResultDto) => {
      this.produtos = produtosPaged.itens;
    });
  }
}
