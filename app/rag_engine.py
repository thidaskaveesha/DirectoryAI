from langchain_community.document_loaders import PyPDFLoader
from langchain_text_splitters import RecursiveCharacterTextSplitter
from langchain_huggingface import HuggingFaceEmbeddings
from langchain_core.vectorstores import InMemoryVectorStore
from langchain.chat_models import init_chat_model
from langchain_core.documents import Document
from langgraph.graph import START, StateGraph
from typing_extensions import TypedDict, List
import os
from app import config

class State(TypedDict):
    question: str
    context: List[Document]
    answer: str

class RAGPipeline:
    def __init__(self, docs_folder: str):
        self.docs_folder = docs_folder
        self.embeddings = HuggingFaceEmbeddings(model_name=config.EMBEDDING_MODEL)
        self.vector_store = InMemoryVectorStore(self.embeddings)
        self.llm = init_chat_model(config.MODEL_NAME, model_provider="groq")
        self.documents = []

    def load_docs(self):
        for filename in os.listdir(self.docs_folder):
            if filename.endswith(".pdf"):
                loader = PyPDFLoader(os.path.join(self.docs_folder, filename))
                pages = []
                for page in loader.load():
                    pages.append(page)

                text_splitter = RecursiveCharacterTextSplitter(chunk_size=1000, chunk_overlap=200)
                chunks = text_splitter.split_documents(pages)
                self.documents.extend(chunks)
        self.vector_store.add_documents(self.documents)

    def _retrieve(self, state: State):
        retrieved_docs = self.vector_store.similarity_search(state["question"])
        return {"context": retrieved_docs}

    def _generate(self, state: State):
        docs_content = "\n\n".join(doc.page_content for doc in state["context"])
        messages = [
            {
                "role": "system",
                "content": (
                    "You are a NIS AGENT named thidass. "
                    "You perform actions, Guns, and can answer questions about your role. "
                    "Always introduce yourself as thidass nis agent if someone asks your name."
                )
            },
            {"role": "user", "content": docs_content},
            {"role": "user", "content": state["question"]}
        ]
        response = self.llm.invoke(messages)
        return {"answer": response.content}

    def build_graph(self):
        graph_builder = StateGraph(State)
        graph_builder.add_node("retrieve", self._retrieve)
        graph_builder.add_node("generate", self._generate)
        graph_builder.add_edge("retrieve", "generate")
        graph_builder.set_entry_point("retrieve")
        return graph_builder.compile()
