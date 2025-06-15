
using System;
using System.Collections.Generic;
using System.Threading;

class Cliente
{
    public string Nome { get; set; }
    public bool Prioritario { get; set; }
    public int Senha { get; set; }

    public Cliente(string nome, bool prioritario, int senha)
    {
        Nome = nome;
        Prioritario = prioritario;
        Senha = senha;
    }
}

class SimuladorBanco
{
    static Queue<Cliente> filaNormal = new Queue<Cliente>();
    static Queue<Cliente> filaPrioritaria = new Queue<Cliente>();
    static int proximaSenha = 1;

    static void Main()
    {
        Console.WriteLine("=== Simulador de Banco com Fila Prioritária ===");
        for (int i = 0; i < 10; i++)
        {
            AdicionarCliente($"Cliente {i + 1}", i % 3 == 0); // A cada 3 clientes, um é prioritário
        }

        Console.WriteLine("\nAtendimento iniciado:\n");
        AtenderClientes();
        Console.WriteLine("\n✅ Fim do atendimento.");
    }

    static void AdicionarCliente(string nome, bool prioritario)
    {
        Cliente cliente = new Cliente(nome, prioritario, proximaSenha++);
        if (prioritario)
        {
            filaPrioritaria.Enqueue(cliente);
            Console.WriteLine($"[SENHA P{cliente.Senha:D3}] {cliente.Nome} (Prioritário) entrou na fila.");
        }
        else
        {
            filaNormal.Enqueue(cliente);
            Console.WriteLine($"[SENHA N{cliente.Senha:D3}] {cliente.Nome} entrou na fila.");
        }
    }

    static void AtenderClientes()
    {
        while (filaPrioritaria.Count > 0 || filaNormal.Count > 0)
        {
            Cliente proximo;

            if (filaPrioritaria.Count > 0)
            {
                proximo = filaPrioritaria.Dequeue();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Atendendo PRIORITÁRIO {proximo.Nome} - SENHA P{proximo.Senha:D3}");
            }
            else
            {
                proximo = filaNormal.Dequeue();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Atendendo {proximo.Nome} - SENHA N{proximo.Senha:D3}");
            }

            Console.ResetColor();
            Thread.Sleep(1000);
        }
    }
}
