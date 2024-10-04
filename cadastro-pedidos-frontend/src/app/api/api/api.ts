export * from './itensPedido.service';
import { ItensPedidoService } from './itensPedido.service';
export * from './pedido.service';
import { PedidoService } from './pedido.service';
export * from './produto.service';
import { ProdutoService } from './produto.service';
export const APIS = [ItensPedidoService, PedidoService, ProdutoService];
