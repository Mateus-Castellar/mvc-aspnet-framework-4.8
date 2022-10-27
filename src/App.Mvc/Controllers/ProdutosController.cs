using App.Business.Models.Fornecedores;
using App.Business.Models.Produtos;
using App.Business.Models.Produtos.Services;
using App.Mvc.ViewModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace App.Mvc.Controllers
{
    public class ProdutosController : CoreController
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IProdutoService _produtoService;
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IMapper _mapper;

        public ProdutosController(IProdutoRepository produtoRepository, IProdutoService produtoService,
            IMapper mapper, IFornecedorRepository fornecedorRepository)
        {
            _produtoRepository = produtoRepository;
            _produtoService = produtoService;
            _mapper = mapper;
            _fornecedorRepository = fornecedorRepository;
        }

        [HttpGet]
        [Route("")]
        [Route("lista-de-produtos")]
        public async Task<ActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<ProdutoViewModel>>(await
                _produtoRepository.ObterProdutosFornecedores()));
        }

        [HttpGet]
        [Route("dados-do-produto/{id:guid}")]
        public async Task<ActionResult> Details(Guid id)
        {
            var produtoViewModel = await ObterProduto(id);

            if (produtoViewModel is null) return HttpNotFound();

            return View(produtoViewModel);
        }

        [HttpGet]
        [Route("novo-produto")]
        public async Task<ActionResult> Create()
        {
            var produtoViewModel = await PopualarFornecedores(new ProdutoViewModel());

            return View(produtoViewModel);
        }

        [HttpPost]
        [Route("novo-produto")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProdutoViewModel produtoViewModel)
        {
            produtoViewModel = await PopualarFornecedores(produtoViewModel);

            if (ModelState.IsValid is false) return View(produtoViewModel);

            await _produtoService.Adicionar(_mapper.Map<Produto>(produtoViewModel));

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("editar-produto/{id:guid}")]
        public async Task<ActionResult> Edit(Guid id)
        {
            var produtoViewModel = await ObterProduto(id);

            if (produtoViewModel is null) return HttpNotFound();

            return View(produtoViewModel);
        }

        [HttpPost]
        [Route("editar-produto/{id:guid}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ProdutoViewModel produtoViewModel)
        {
            if (ModelState.IsValid is false) return View(produtoViewModel);

            await _produtoService.Atualizar(_mapper.Map<Produto>(produtoViewModel));

            return RedirectToAction("Index");
        }


        [HttpGet]
        [Route("excluir-produto/{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var produtoViewModel = await ObterProduto(id);

            if (produtoViewModel is null) return HttpNotFound();

            return View(produtoViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [Route("excluir-produto/{id:guid}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            var produtoViewModel = await ObterProduto(id);

            if (produtoViewModel is null) return HttpNotFound();

            await _produtoService.Remover(id);

            return RedirectToAction("Index");
        }

        private async Task<ProdutoViewModel> PopualarFornecedores(ProdutoViewModel produto)
        {
            produto.Fornecedores = _mapper.Map<IEnumerable<FornecedorViewModel>>(await
                _fornecedorRepository.ObterTodos());

            return produto;
        }

        private async Task<ProdutoViewModel> ObterProduto(Guid id)
        {
            var produto = _mapper.Map<ProdutoViewModel>(await
                _produtoRepository.ObterProdutoFornecedor(id));

            produto.Fornecedores = _mapper.Map<IEnumerable<FornecedorViewModel>>(await
                _fornecedorRepository.ObterTodos());

            return produto;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _produtoRepository.Dispose();
                _produtoService.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
