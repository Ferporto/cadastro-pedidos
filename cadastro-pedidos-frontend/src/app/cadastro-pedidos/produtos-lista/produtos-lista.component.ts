import {AfterViewInit, Component, TemplateRef, ViewChild} from '@angular/core';
import {MatDialog} from "@angular/material/dialog";

import {CadastroPedidosService} from "../cadastro-pedidos.service";
import { ProdutoOutput, ProdutoOutputPagedResultDto, ProdutoService } from '../../api';
import { ProdutosEditorModalComponent } from './produtos-editor-modal/produtos-editor-modal.component';


@Component({
  selector: 'app-produtos-lista',
  templateUrl: './produtos-lista.component.html',
  styleUrls: ['./produtos-lista.component.scss']
})
export class ProdutosListaComponent implements AfterViewInit {
  @ViewChild('ProdutosListHeader') private headerTemplate!: TemplateRef<any>;

  public columns: string[] = ['actions', 'nomeProduto', 'valor'];
  public produtos: ProdutoOutput[] = [];

  constructor(private cadastroPedidosService: CadastroPedidosService,  private matDialog: MatDialog,
              private service: ProdutoService) {
    this.getProdutos();
  }

  ngAfterViewInit(): void {
    this.emitHeaderTemplate();
  }

  public openProdutoEditor(produto?: ProdutoOutput): void {
    this.matDialog.open(ProdutosEditorModalComponent, {
      data: produto,
      hasBackdrop: true,
      height: 'calc(100% - 64px)',
      width: '40%',
      position: {
        right: '0',
        bottom: '0',
      }
    }).afterClosed().subscribe(() => {
      this.getProdutos();
    });
  }

  public update(produto: ProdutoOutput): void {
    this.openProdutoEditor(produto);
  }

  public delete(produto: ProdutoOutput): void {
    this.service.produtosIdDelete(produto.id).subscribe(() => {
      this.getProdutos();
    });
  }

  private getProdutos(): void {
    this.service.produtosGet().subscribe((produtosPaged: ProdutoOutputPagedResultDto) => {
      this.produtos = produtosPaged.itens;
    });
  }

  private emitHeaderTemplate(): void {
    this.cadastroPedidosService.headerTemplate.next(this.headerTemplate);
  }
}
